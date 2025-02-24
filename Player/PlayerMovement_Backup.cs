//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement_Backup : Subject, IObserver
//{

//    public IdleState idleState;
//    public RunState runState;
//    public AirState airState;
//    public AttackState attackState;
//    public HurtState hurtState;

//    State state;

//    public Animator animator;

//    public Rigidbody2D body;
//    public CapsuleCollider2D groundCheck;
//    public LayerMask groundMask;
//    [SerializeField] float speed;
//    public float maxSpeed;

//    public float acceleration;

//    public bool isAttacking;
//    public bool takeDamage = false;
//    public bool isLocked = false;

//    [Range(0,1)]
//    [SerializeField] float groundDecay;
//    public bool isGrounded { get; private set; }
//    public float xInput { get; private set; }
//    public float yInput { get; private set; }

//    [SerializeField] Subject playerHealth;

//    // Start is called before the first frame update
//    void Start()
//    {
//        idleState.Setup(body, animator, this);
//        runState.Setup(body, animator, this);
//        airState.Setup(body, animator, this);
//        attackState.Setup(body, animator, this);
//        hurtState.Setup(body, animator, this);
//        state = idleState;        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        GetInput();         
//        HandleJump();

//        HandleAttack();        

//        SelectState();
//        state.Do();
//    }    

//    void FixedUpdate()
//    {
//        CheckGround();
//        ApplyFriction();
//        HandleXMovement();
//    }

//    void GetInput() // receive input from player
//    {
//        xInput = Input.GetAxis("Horizontal");
//        yInput = Input.GetAxis("Vertical");
//    }    

//    void HandleAttack()
//    {
//        if (Input.GetButton("Fire1"))
//        {
//            isAttacking = true;            
//        }
//        else
//        {
//            isAttacking = false;            
//        }
//    }

//    void HandleXMovement()
//    {
//        if (isAttacking) return;
//        if (Mathf.Abs(xInput) > 0)
//        {
//            float increment = xInput * acceleration;
//            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -maxSpeed, maxSpeed);
//            body.velocity = new Vector2(newSpeed, body.velocity.y);
//        }
//        FaceInput();
//    }

//    void HandleJump() // handle jump command
//    {
//        if (isAttacking) return;
//        if (Input.GetButtonDown("Jump")  && isGrounded)
//        {
//            NotifyObserver(PlayerAction.Jump);
//            body.velocity = new Vector2(body.velocity.x, airState.jumpForce);
//        }
//    }

//    void ApplyFriction() // apply friction to velocity
//    {
//        if(isGrounded && xInput == 0 && body.velocity.y <= 0)
//        {
//            body.velocity *= groundDecay;
//        }
//    }   

//    void CheckGround() // check if player is on the ground 
//    {
//        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
//    }

//    void FaceInput()
//    {
//        float direction = Mathf.Sign(xInput) * 1f;
//        transform.localScale = new Vector3(direction, 1f, 1f);
//    }    

//    void SelectState()
//    {
//        State oldState = state;

//        if (isLocked)
//            return;

//        if (Input.GetKeyDown(KeyCode.K))
//        {
//            state = attackState;
//        }

//        if (isGrounded)
//        {
//            if (xInput == 0)
//            {         
                
                
//                    state = idleState;
                
//            }
//            else
//            {               
                
//                    state = runState;
                
//            }
//        }
//        else
//        {
//            state = airState;
//        }

//        //if (isGrounded)
//        //{
//        //    if (xInput == 0)
//        //    {
//        //        if (isAttacking)
//        //        {
//        //            state = attackState;
//        //            //CountAttackTime();
//        //        }
//        //        else
//        //        {
//        //            state = idleState;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        if (isAttacking)
//        //        {
//        //            state = attackState;
//        //            //CountAttackTime();
//        //        }
//        //        else
//        //        {
//        //            state = runState;
//        //        }
//        //    }
//        //}
//        //else
//        //{
//        //    state = airState;
//        //}


//        if (oldState != state || oldState.isComplete)
//        {
//            oldState.Exit();
//            state.Initialise();
//            state.Enter();
//        }  

        
//    }

//    private IEnumerator UninterruptibleStateRoutine()
//    {
//        isLocked = true;
//        Debug.Log("Entering uninterruptible state...");

//        // Simulate uninterruptible action
//        yield return new WaitForSeconds(0.7f);

//        Debug.Log("Uninterruptible state completed.");
//        isLocked = false;
//        takeDamage = false;

//        state = idleState;
//    }

//    public void OnNotify(PlayerAction action)
//    {
//        if(action == PlayerAction.Hurt)
//        {
//            takeDamage = true;
//            state = hurtState;
//            state.Enter();
//            StartCoroutine(UninterruptibleStateRoutine());
//        }
//    }

//    void OnEnable()
//    {
//        playerHealth.AddObserver(this);
        
//    }

//    void OnDisable()
//    {
//        playerHealth.RemoveObserver(this);        
//    }
//}
