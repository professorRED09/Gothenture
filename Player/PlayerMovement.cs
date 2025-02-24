using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Subject, IObserver
{
    [Header("FSM")]
    public IdleState idleState;
    public RunState runState;
    public AirState airState;
    public AttackState attackState;
    public ParryState parryState;
    public HurtState hurtState;
    public DeadState deadState;
    State state;

    [Header("Other Refs")]
    public PlayerHealth status;
    private PlayerDodge dodgeMech;
    //public PlayerCombat combatMech;
    public Animator anim;
    public LayerMask groundMask;

    [Header("Subjects")]
    [SerializeField] Subject playerHealth;
    [SerializeField] Subject dialogueManager;

    Rigidbody2D rb;
    CapsuleCollider2D groundCheck;

    
    [Header("Settings")]
    [SerializeField] float speed;
    public float maxSpeed;
    public float acceleration;
    public float slideSpeed;

    public bool isAttacking;
    //public bool isParrying;
    public bool canControl;
    public bool takeDamage = false;
    public bool isLocked = false;
    public bool isDead;

    bool isFacingRight = true;   

    public float groundCheckRadius;

    [Range(0, 1)]
    [SerializeField] float groundDecay;
    public bool isGrounded { get; private set; }
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    
    [Header("KB Force")]
    [SerializeField] float kBForceX;
    [SerializeField] float kBForceY;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
        groundCheck = GetComponent<CapsuleCollider2D>();
        //dodgeMech = GetComponent<PlayerDodge>();        

        // set up all player's state
        idleState.Setup(rb, anim, this);
        runState.Setup(rb, anim, this);
        airState.Setup(rb, anim, this);
        attackState.Setup(rb, anim, this);
        parryState.Setup(rb, anim, this);
        hurtState.Setup(rb, anim, this);
        deadState.Setup(rb, anim, this);

        state = idleState;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();        

        GetInput();
        HandleJump();             

        SelectState();
        state.Do();
    }

    void FixedUpdate()
    {
        if (isDead)
            return;

        CheckGround();
        ApplyFriction();
        HandleXMovement();
    }

    // check if player is dead
    void CheckStatus() 
    {
        if (status.isDead)
        {
            state = deadState;    // enter dead state
            state.Enter();
        }
    }

    // get input from player
    void GetInput() 
    {
        if (!canControl)
            return;

        // Get movement input
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        // Check for combat input
        if (Input.GetKey(KeyCode.K))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }        
    }    

    // make player stays in idleState & disable all control and movement
    public void StopMoving()
    {
        state = idleState;
        state.Enter();
        canControl = false;
        rb.velocity = Vector2.zero;
    }

    // handle horizontal movement
    void HandleXMovement() 
    {
        // if player is attacking or can't control, return
        if (isAttacking || !canControl) return;

        // if player moves in x axis, gradually increase speed to move the character
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(rb.velocity.x + increment, -maxSpeed, maxSpeed);
            rb.velocity = new Vector2(newSpeed, rb.velocity.y);
        }

        // flip sprite for each direction from the input
        FlipSprite();
    }

    // handle jump command
    void HandleJump() 
    {
        // if player is attacking or can't control, return
        if (isAttacking || !canControl) return;

        // if player presses jump button and is on the ground, perform the jump 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            NotifyObserver(PlayerAction.Jump);
            rb.velocity = new Vector2(rb.velocity.x, airState.jumpForce);
        }
    }

    void ApplyFriction() // apply friction to velocity
    {
        if (isGrounded && xInput == 0 && rb.velocity.y <= 0)
        {
            rb.velocity *= groundDecay;
        }
    }    

    void CheckGround() // check if player is on the ground 
    {        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckRadius, groundMask);
        if (hit.collider != null && hit.normal == Vector2.up) // Ensure normal points up
        {
            isGrounded =  true;
        }
        else
        {
            isGrounded = false;
        }

    }

    // for flipping character's sprite when turn to left or right
    void FlipSprite()
    {        
        if (isFacingRight && xInput < 0f || !isFacingRight && xInput > 0f)
        {
            isFacingRight = !isFacingRight;
            //tempDir = isFacingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1f;
            transform.localScale = tempScale;
        }
    }

    //void FaceInput() // flip sprite on each direction
    //{
    //    float direction = Mathf.Sign(xInput);
    //    transform.localScale = new Vector3(direction, 1f, 1f);
    //    print(xInput);
    //}   

    // select state for player based on each conditions
    void SelectState()
    {
        // if player is dead, then don't switch to any state
        if (status.isDead)
            return;

        State oldState = state;

        // if player is taking damage, then don't switch to any state
        if (isLocked)
            return;

        if (isGrounded)
        {            
            if (xInput == 0)
            {
                if (isAttacking)
                {
                    state = attackState;                    
                } 
                //else if (isParrying)
                //{
                //    state = parryState;
                //}
                else
                {
                    state = idleState;
                }
            }
            else
            {
                if (isAttacking)
                {
                    state = attackState;                    
                }
                //else if (isParrying)
                //{
                //    state = parryState;
                //}
                else
                {
                    state = runState;
                }
            }
        }
        else
        {
            state = airState;
        }      

        if (oldState != state || oldState.isComplete)
        {
            oldState.Exit();
            state.Initialise();
            state.Enter();
        }
    }

    private IEnumerator Paralyzed(float time)
    {
        isLocked = true;
        canControl = false;
        Debug.Log("CAN'T MOVE");

        // Simulate uninterruptible action
        yield return new WaitForSeconds(time);

        Debug.Log("CAN MOVE");
        isLocked = false;
        takeDamage = false;
        canControl = true;
        state = idleState;
    }

    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Hurt):
                takeDamage = true;
                state = hurtState;
                state.Enter();
                StartCoroutine(Paralyzed(0.7f));
                return;

            case (PlayerAction.Pause):
                canControl = false;
                //dodgeMech.enabled = false;
                //combatMech.enabled = false;
                return;

            case (PlayerAction.Resume):
                canControl = true;
                //dodgeMech.enabled = true;
                //combatMech.enabled = true;
                return;

            case (PlayerAction.Talk):
                StopMoving();
                //dodgeMech.enabled = false;
                //combatMech.enabled = false;
                return;

            case (PlayerAction.Leave):
                canControl = true;
                //dodgeMech.enabled = true;
                //combatMech.enabled = true;
                return;

            default:
                return;

        }
    }

    // this function will make player gets knockback when taking any damage
    public void Knockback(Transform enemy)
    {
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.AddForce(Vector2.up * kBForceY, ForceMode2D.Impulse);
        rb.AddForce(direction * kBForceX, ForceMode2D.Impulse);
    } 

    void OnEnable()
    {
        playerHealth.AddObserver(this);
        dialogueManager.AddObserver(this);
    }

    void OnDisable()
    {
        playerHealth.RemoveObserver(this);
        dialogueManager.RemoveObserver(this);
    }
}
