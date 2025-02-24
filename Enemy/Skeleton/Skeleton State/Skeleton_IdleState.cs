using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : EnemyState
{
    public AnimationClip anim;
    public override void Enter()
    {        
        animator.Play(anim.name);
    }

    public override void Do()
    {
        body.velocity = Vector2.down;
        if (input.DistanceCalculate() <= 10f)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
