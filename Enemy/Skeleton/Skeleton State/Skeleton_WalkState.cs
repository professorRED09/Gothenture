using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_WalkState : EnemyState
{
    public AnimationClip anim;
    [SerializeField] private float walkSpeed;
    public override void Enter()
    {       
        animator.Play(anim.name); //play the given animation
    }

    public override void Do()
    {        
        body.velocity = new Vector2(input.sentDirection * walkSpeed, body.velocity.y);  //enemy will walk with given direction and speed

        if (input.DistanceCalculate() <= 1.9f || input.DistanceCalculate() > 10f || input.isOutOfArea) //if enemy is not alert anymore, then complete the state
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
