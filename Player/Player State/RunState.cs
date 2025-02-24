using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{    
    public AnimationClip anim;
    public override void Enter()
    {
        animator.Play(anim.name); // play run animation
    }

    public override void Do()
    {
        if (!input.isGrounded) // if player is no longer on the ground, then complete this state.
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
