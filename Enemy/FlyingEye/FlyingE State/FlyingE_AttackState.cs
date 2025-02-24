using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingE_AttackState : FlyingEyeState
{
    public AnimationClip anim;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    //public float fireballCooldown = 2f;
    //private float lastFireballTime;

    public override void Enter()
    {
        animator.Play(anim.name);        

    }

    public override void Do()
    {
        body.velocity = Vector2.zero;        

        if (flyingE.DistanceCalculate() > flyingE.alertRange)
        {
            isComplete = true;
        }        
    }

    //void CastFireball()
    //{
    //    // Instantiate the fireball.
    //    GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
    //    // Initialize the fireball to move toward the player.
    //    fireball.GetComponent<Fireball>().Initialize(flyingE.player.transform.position);
    //    // Update the last fireball time.
    //    lastFireballTime = Time.time;

    //}

    void CastFireball()
    {
        // Instantiate the fireball.
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        // Initialize the fireball to move toward the player.
        fireball.GetComponent<Fireball>().Initialize(flyingE.player.transform.position);
    }
}
