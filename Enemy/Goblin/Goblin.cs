using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [Header("FSM Ref")]
    public Goblin_PatrolState patrolState;
    public Goblin_AlertState alertState;
    GoblinState state;

    [Header("Ref")]
    public Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    EnemyHealth status;

    [SerializeField] bool foundPlayer;
    bool isFacingRight = true;
    float sentDirection = 1;

    [Header("Setting")]
    //[SerializeField] Vector3 posOffset;
    [SerializeField] float patrolLength;    
    [SerializeField] LayerMask playerMask;

    Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        //setup varirables
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        status = GetComponent<EnemyHealth>();

        //setup state machine
        patrolState.Setup(rb, anim, this);
        alertState.Setup(rb, anim, this);
        state = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        //FLIPING
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            Flip();  // Player is to the right and enemy is facing left, so flip
        }
        // Check if the player is to the left of the enemy
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            Flip();  // Player is to the left and enemy is facing right, so flip
        }
    }

    void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, patrolLength, playerMask);
        if (hit.collider != null)
        {
            foundPlayer = true;
            print("DDD");
        }
    }
    
    public void Flip()
    {
        if (status.isDead)
            return;
        isFacingRight = !isFacingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;                          // Flip the X-axis scale
        transform.localScale = direction;           // Apply the flipped scale
        GiveDirection();
    }
    public void GiveDirection()
    {
        sentDirection *= -1;
    }

    void SelectState()
    {
        GoblinState oldState = state;

        if (status.isDead)
            return;

        if (foundPlayer)
        {
            state = alertState;
        }
        else
        {
            state = patrolState;
        }

        if (oldState != state || oldState.isComplete)
        {
            oldState.Exit();
            state.Initialise();
            state.Enter();
        }

    }

    void OnDrawGizmos()
    {   
        // Visualize the ray in the scene view
        Gizmos.color = Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * patrolLength));
    }
}
