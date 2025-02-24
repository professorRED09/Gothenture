using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingE_FleeState : FlyingEyeState
{
    public AnimationClip anim;
    [SerializeField] private float fleeSpeed;
    public override void Enter()
    {
        animator.Play(anim.name); //play the given animation
    }

    public override void Do()
    {
        body.velocity = new Vector2(/*flyingE.sentDirection **/ fleeSpeed, body.velocity.y);  //enemy will walk with given direction and speed

        if (flyingE.DistanceCalculate() <= 1.9f || flyingE.DistanceCalculate() > 10f) //if enemy is not alert anymore, then complete the state
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
