using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingE_DeadState : FlyingEyeState
{
    public AnimationClip anim;
    //public CircleCollider2D col;
    public GameObject enemy;
    public GameObject item;
    bool isQuitting = false;
    public override void Enter()
    {
        animator.Play(anim.name); //play the given animation        
        //Instantiate(key, transform.position, Quaternion.identity);
    }

    public override void Do()
    {
        //col.enabled = false;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.gravityScale = 10;        
        Destroy(enemy, 2f);
        //DropItem();
    }

    public override void Exit()
    {

    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
