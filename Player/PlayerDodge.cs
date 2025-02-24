using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour, IObserver
{
    Subject playerInteract;
    Subject dialogueMananger;

    [SerializeField] private bool canDodge = true;
    public float dodgeSpeed = 10f; // Speed of the dodge
    public float dodgeDuration = 0.3f; // How long the dodge lasts
    public float dodgeCooldown = 1f; // Time before next dodge    
    
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    public float groundCheckRadius;

    [Range(0, 1)]
    [SerializeField] float groundDecay;
    public LayerMask groundMask;
    private bool isGrounded;

    [SerializeField] float detectLength;
    
    void Awake()
    {
        playerInteract = gameObject.transform.GetChild(4).GetComponentInChildren<PlayerInteraction>();
        dialogueMananger = FindObjectOfType<DialogueManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();        
    }

    void Update()
    {
        CheckGround();

        if (Input.GetKeyDown(KeyCode.F) && canDodge && isGrounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), detectLength, groundMask);
            if (hit.collider != null)
            {
                return;
            }
            StartCoroutine(DodgeRoll());
        }
    }

    IEnumerator DodgeRoll()
    {
        if (canDodge == false)
            yield return null;
        canDodge = false;        
        
        Vector2 originalVelocity = rb.velocity;
         
        float moveDirection = transform.localScale.x; 
        Vector2 dodgeVector = new Vector2(moveDirection, 0).normalized * dodgeSpeed;

        
        Physics2D.IgnoreLayerCollision(6, 8, true);  // ignore collision between player(6) and enemy(8)        
        
        rb.velocity = dodgeVector;                
        yield return new WaitForSeconds(dodgeDuration);        
        rb.velocity = originalVelocity;        
        
        Physics2D.IgnoreLayerCollision(6, 8, false);   
        
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }
    void CheckGround() // check if player is on the ground 
    {
        //isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius, groundMask);
        if (hit.collider != null && hit.normal == Vector2.up) // Ensure normal points up
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Pause):
                canDodge = false;                
                return;

            case (PlayerAction.Resume):
                canDodge = true;                
                return;

            case (PlayerAction.Talk):
                canDodge = false;
                print("YOU CAN NOT DODGE");
                return;

            case (PlayerAction.Leave):
                canDodge = true;
                print("YOU CAN DODGE");
                return;

            default:
                return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
        Gizmos.color = Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * detectLength));
    }

    void OnEnable()
    {
        playerInteract.AddObserver(this);
        dialogueMananger.AddObserver(this);
    }

    void OnDisable()
    {
        playerInteract.RemoveObserver(this);
        dialogueMananger.RemoveObserver(this);
    }
}
