using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{    
    public AnimationClip anim;
    public override void Enter()
    {
        animator.Play(anim.name);               // play dead animation
        input.canControl = false;               // disable input from player
        body.velocity = new Vector2(0, 0);      // disable player character from moving
    }

    public override void Do()
    {
        
    }

    public override void Exit()
    {

    }
}
