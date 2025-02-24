using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    FlyingEyeState state;
    public FlyingE_IdleState idleState;
    public FlyingE_AttackState attackState;
    public FlyingE_DeadState deadState;

    [SerializeField] Animator animator;
    public Rigidbody2D body;

    public Transform player;
    public float alertRange;
 
    public float sentDirection;

    [SerializeField] float distance; //for easy checking on an inspector 

    EnemyHealth status;

    private void Awake()
    {
        status = GetComponent<EnemyHealth>();
    }

    // Start is called before the first frame update
    void Start()
    {
        idleState.Setup(body, animator, this);
        attackState.Setup(body, animator, this);        
        deadState.Setup(body, animator, this);

        state = idleState;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        DistanceCalculate();
        distance = DistanceCalculate();

        if (state.isComplete)
        {
            SelectState();
        }
        state.Do();     
    }    

    // check if the enemy is dead, change to dead state 
    void CheckStatus()
    {
        if (status.isDead)
        {
            state = deadState;
            state.Enter();
        }

    }

    //calculate distance between player and enemy to decide when to start running toward player 
    public float DistanceCalculate() 
    {
        
        float distance = Mathf.Abs(player.position.x - transform.position.x);

        return distance;
    }

    void SelectState()
    {
        //if (status.isDead)
        //    return;

        //if (DistanceCalculate() <= alertRange)
        //{
        //    if (DistanceCalculate() > fleeRange)
        //    {
        //        state = attackState;
        //    }
        //    else if (DistanceCalculate() <= fleeRange)
        //    {
        //        state = fleeState;
        //    }

        //}
        //else
        //{
        //    state = idleState;
        //}
        //state.Enter();

        if (status.isDead)
            return;

        if (DistanceCalculate() <= alertRange)
        {
            state = attackState;
        }
        else
        {
            state = idleState;
        }
        state.Enter();


    }


}






