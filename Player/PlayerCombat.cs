using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : Subject, IObserver
{
    [Header("Ref")]
    public PlayerHealth status;
    Subject playerInteract;
    Subject dialogueManager;
    AudioSource audioSource;
    public AudioClip swingSound;
    public AudioClip slashSound;

    [Header("Melee")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float slashRange;
    [SerializeField] LayerMask enemyLayers;    

    [Header("Shoot")]
    [SerializeField] GameObject spellballPrefab;
    [SerializeField] float spellballSpeed;
    public float aimRange = 10f;                 // Range within which the player can auto-aim
    public Transform spellCastPos;               // The player's weapon or aim pivot
    private Transform target;                    // The current enemy the player is targeting

    [Header("SP Bar")]
    [SerializeField] float maxSP;
    public Slider Slider;
    [SerializeField] float currentSP;
    [SerializeField] float sPCost;

    [SerializeField] bool canCombat;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        playerInteract = FindObjectOfType<PlayerInteraction>();
    }

    void Start()
    {        
        currentSP = maxSP;
        ChangeSPBar(currentSP, maxSP);
    }

    void Update()
    {
        if (status.isDead)
            return;

        FindNearestEnemy();         
        ChangeSPBar(currentSP, maxSP);
        if (Input.GetKeyDown(KeyCode.O) && canCombat)
        {
            print("CAST MAHO!");
            CastSpellball();
        }
    }

    void ChangeSPBar(float currentValue, float maxValue)
    {
        Slider.value = currentValue / maxValue;
    }

    public void RestoreSP(float amount)
    {
        if (currentSP + amount > maxSP) // prevent exeeded Sp
        {
            currentSP = maxSP;
        }
        else
        {
            currentSP += amount;
        }
        print("MANA UP");
    }

    void HandleAttack()
    {        
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, slashRange, enemyLayers);
        
        foreach (Collider2D enemy in hits)
        {
            if (enemy.GetComponent<IHealth>() != null)
            {
                enemy.GetComponent<IHealth>().TakeDamage(10.0f);
                //audioSource.clip = slashSound;
                //audioSource.Play();
            }
            else if (enemy.GetComponent<Fireball>() != null)
            {
                enemy.GetComponent<Fireball>().GotParried();
            }
            else
            {
                print("YOU HIT NOTHING");
                //audioSource.clip = swingSound;
                //audioSource.Play();
            }            
            //print("HIT ENEMY");            
            //enemy.GetComponent<IHealth>().TakeDamage(10.0f);
        }

        audioSource.clip = swingSound;
        audioSource.Play();

    }


    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Talk):
                canCombat = false;
                return;
            case (PlayerAction.Leave):
                canCombat = true;
                return;
            case (PlayerAction.Pause):
                canCombat = false;
                return;
            case (PlayerAction.Resume):
                canCombat = true;
                return;
            default:
                return;
        }
    }

    void CastSpellball()
    {
        if (target == null || currentSP < sPCost) return;

        GameObject bullet = Instantiate(spellballPrefab, spellCastPos.position, spellCastPos.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - spellCastPos.position).normalized;
        rb.velocity = direction * spellballSpeed;
        print(direction);
        currentSP -= sPCost;
    }    

    void FindNearestEnemy()
    {
        // Find all enemies within the aim range
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(spellCastPos.position, aimRange, enemyLayers);

        float closestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector2.Distance(spellCastPos.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                nearestEnemy = enemy.transform;
            }
        }

        target = nearestEnemy; // Update the target enemy
    }

    void AimAtTarget()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, slashRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spellCastPos.position, aimRange);
    }

    void OnEnable()
    {
        playerInteract.AddObserver(this);
        dialogueManager.AddObserver(this);
    }

    void OnDisable()
    {
        playerInteract.RemoveObserver(this);
        dialogueManager.RemoveObserver(this);
    }
}
