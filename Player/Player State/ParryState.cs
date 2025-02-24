using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryState : State
{
    public AnimationClip anim;

    public override void Enter()
    {
        //print("PLAY PARRY ANIM");
        animator.Play(anim.name);
    }

    public override void Do()
    {
        body.velocity = Vector2.zero; // player can't move when perform parry command

    }

    public override void Exit()
    {

    }
}
