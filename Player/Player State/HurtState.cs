using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    public AnimationClip anim;
    public override void Enter()
    {
        body.velocity = Vector2.zero;        
        animator.Play(anim.name);        
    }

    public override void Do()
    {        
        
    }

    public override void Exit()
    {

    }
}
