using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGhoul_PatrolState : BGhoulState
{
    [Header("Reference")]
    
    //public AnimationClip idleAnim;
    //public AnimationClip walkAnim;

    public Transform pointA;
    public Transform pointB;    

    [Header("Settings")]
    [SerializeField] float speed;    

    public override void Enter()
    {
        print("START PATROL");
        rb.velocity = new Vector2(-speed, 0);

    }

    public override void Do()
    {
        StartPatrol();
        if (bGhoul.metPlayer)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }

    void StartPatrol()
    {
        //anim.Play(walkAnim.name);
        
        if (this.gameObject.transform.position.x <= pointB.position.x)
        {
            //print("TurnLeft");
            rb.velocity = new Vector2(+speed, 0);
            //bGhoul.FlipX();
           
        }
        else if (this.gameObject.transform.position.x >= pointA.position.x)
        {
            //print("TurnRight");            
            rb.velocity = new Vector2(-speed, 0);
            //bGhoul.FlipX();
        }
    }        
}
