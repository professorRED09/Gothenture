using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : State
{
    public float jumpForce;
    public AnimationClip jumpAnim;
    public AnimationClip fallAnim;
    public override void Enter()
    {

        if (body.velocity.y < 0)
        {
            animator.Play(fallAnim.name);
        }
        else
        {
            animator.Play(jumpAnim.name);
        }
        
    }

    public override void Do()
    {       

        if (input.isGrounded)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {

    }
}
