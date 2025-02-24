using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{   
    public AnimationClip anim;
        
    public override void Enter()
    {
        //print("PLAY ATTACK ANIM");
        animator.Play(anim.name);
    }

    public override void Do()
    {
        body.velocity = Vector2.zero; // player can't move when perform attack command

    }

    public override void Exit()
    {

    }
}
