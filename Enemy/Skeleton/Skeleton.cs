using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [Header("FSM Ref")]
    public Skeleton_IdleState idleState;
    public Skeleton_WalkState walkState;    
    public Skeleton_AttackState attackState;
    public Skeleton_DeadState deadState;
    EnemyState state;

    [Header("Ref")]
    [SerializeField] Animator animator;    
    public Rigidbody2D body;
    public Transform player;
    public LayerMask playerLayer;
    [SerializeField] Transform bound_negX;
    [SerializeField] Transform bound_posX;
    EnemyHealth status;

    [Header("Setting")]
    [SerializeField] private float patrolLength;
    [SerializeField] bool isAlert;
    [SerializeField] float attackRange;

    private bool isFacingRight = true;
    private float distance; //for easy checking on an inspector 
    public bool isOutOfArea;
    public float sentDirection = 1;    

    private void Awake()
    {
        status = GetComponent<EnemyHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        idleState.Setup(body, animator, this);
        walkState.Setup(body, animator, this);
        attackState.Setup(body, animator, this);
        deadState.Setup(body, animator, this);
        state = idleState;               
    }

    // Update is called once per frame
    void Update()
    {
        //CheckArea();
        DistanceCalculate();
        distance = DistanceCalculate();
        CheckStatus();
        LookForPlayer();

        if (state.isComplete)
        {
            SelectState();
        }
        state.Do();
        

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

    void LookForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), patrolLength, playerLayer);
        if (hit.collider != null)
        {
            isAlert = true;            
        }
        
    }

    public void GiveDirection()
    {
        sentDirection *= -1;
    }

    // check if the enemy is dead, then change to dead state
    void CheckStatus()
    {
        if (status.isDead)
        {
            state = deadState;
            state.Enter();
        }            
    }

    //void CheckArea()
    //{
    //    if (transform.position.x <= bound_negX.position.x || transform.position.x >= bound_posX.position.x)
    //    {
    //        isOutOfArea = true;
    //    }
    //    else
    //    {
    //        isOutOfArea = false;
    //    }


    //    //if (!isAlert)
    //    //{
    //    //    if (transform.position.x <= bound_negX.position.x || transform.position.x >= bound_posX.position.x)
    //    //    {
    //    //        isOutOfArea = true;
    //    //    }            
    //    //}
    //    //else
    //    //{
    //    //    if (transform.position.x <= bound_negX.position.x || transform.position.x >= bound_posX.position.x)
    //    //    {
    //    //        isOutOfArea = true;
    //    //    }
    //    //}

    //}

    // This function will flip sprite based on the given direction
    public void Flip()
    {
        if (status.isDead)
            return;
        isFacingRight = !isFacingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;  // Flip the X-axis scale
        transform.localScale = direction;  // Apply the flipped scale
        GiveDirection();
    }

    // This function does damage to player when the enemy collides with them
    void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponentInChildren<PlayerHealth>().TakeDamage(3);
            col.gameObject.GetComponent<PlayerMovement>().Knockback(transform);
        }
    }

    //calculate distance between player and enemy to decide when to start running toward player 
    public float DistanceCalculate() 
    {
        //float distance = Vector2.Distance(player.transform.position, transform.position);
        float distance = Mathf.Abs(player.position.x - transform.position.x);
       
        return distance;
    }

    // select state for the enemy based on each conditions
    void SelectState()
    {
        if (status.isDead) {
            return;
        }

        if (isAlert)
        {
            if (!isOutOfArea)
            {
                state = walkState;
                // if enemy stays out of its zone, stop chasing
                if (transform.position.x <= bound_negX.position.x || transform.position.x >= bound_posX.position.x)
                {
                    isOutOfArea = true;
                }
                else
                {
                    // if player stays near enemy, attack them
                    if (DistanceCalculate() <= attackRange)
                    {
                        state = attackState;
                    }
                }
                
            }
            else
            {                
                // if player stays in enemy zone, start chasing again
                if (player.position.x <= bound_posX.position.x && player.position.x >= bound_negX.position.x)
                {
                    isOutOfArea = false;
                }
                else
                {                    
                    // if player stays near enemy, attack them
                    if (DistanceCalculate() <= attackRange)
                    {
                        state = attackState;
                    }
                    else
                    {
                        state = idleState;
                    }
                }                
            }
        }
        else
        {
            state = idleState;
        }      
        state.Enter();        
    }

    // Visualize the ray in the scene view
    void OnDrawGizmos()
    {        
        Gizmos.color = Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * patrolLength));
    }
}
