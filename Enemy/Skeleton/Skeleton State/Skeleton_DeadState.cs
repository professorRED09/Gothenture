using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_DeadState : EnemyState
{
    public AnimationClip anim;
    public CapsuleCollider2D col;
    public GameObject enemy;
    public GameObject key;
    bool isQuitting = false;
    public override void Enter()
    {       
        animator.Play(anim.name); //play the given animation
        //Instantiate(key, transform.position, Quaternion.identity);
    }

    public override void Do()
    {
        col.enabled = false;
        body.constraints = RigidbodyConstraints2D.FreezePosition;
        Destroy(enemy, 2f);
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
            Instantiate(key, transform.position, Quaternion.identity);
        }
    }    
}
