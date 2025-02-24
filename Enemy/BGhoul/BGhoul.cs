using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGhoul : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] BGhoul_PatrolState patrolState;
    [SerializeField] BGhoul_AlertState alertState;
    public Transform patrolSight;

    public LayerMask player;
    public Rigidbody2D rb;
    [SerializeField] Animator animator;
    BGhoulState state;
    SpriteRenderer sprite;

    [Header("Settings")]
    public float patrolLength;
    [SerializeField] float distance; //for easy checking on an inspector 
    public float sentDirection;
    public bool takeDamage = false;
    public bool isLocked = false;
    public bool metPlayer;
    [SerializeField] Vector2 direction;

    //EnemyHealth status;
    

    //private void Awake()
    //{
    //    status = GetComponent<EnemyHealth>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        patrolState.Setup(rb, animator, this);  
        alertState.Setup(rb, animator, this);

        state = patrolState;
        state.Enter();

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();

        // using raycast to detect player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, patrolLength, player);
        if (hit.collider != null)
        {            
            metPlayer = true;
        }

        // when any state is completed. select new state
        if (state.isComplete)
        {
            SelectState();
        }
        state.Do();

        // flip sprite when bGhoul move to left side
        sprite.flipX = rb.velocity.x < 0f;      
    }    

    //void CheckStatus()
    //{
    //    if (status.isDead)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    RaycastHit2D hits = Physics2D.Raycast(patrolSight.position, Vector2.right, patrolLength, player);

    //    if (collision.gameObject.GetComponent<LayerMask>() == player)
    //    {
    //        metPlayer = true;
    //    }
    //}

    // this function changes direction for bGhoul based on sprite
    void ChangeDirection()
    {        
        if(sprite.flipX == true) {
            direction = new Vector2(-1, 0);
        }
        else {
            direction = new Vector2(1, 0);
        }
    }

    void SelectState()
    {
        BGhoulState oldState = state;
        //if (status.isDead)
        //    return;

        if (metPlayer)
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
    {        // Visualize the ray in the scene view
        Gizmos.color = Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * patrolLength));
    }




}
