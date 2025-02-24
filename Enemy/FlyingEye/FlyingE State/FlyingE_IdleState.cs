using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingE_IdleState : FlyingEyeState
{
    public AnimationClip anim;
    public override void Enter()
    {
        animator.Play(anim.name);
    }

    public override void Do()
    {
        body.velocity = Vector2.zero;
        if (flyingE.DistanceCalculate() <= flyingE.alertRange)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
