using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AttackState : EnemyState
{
    public AnimationClip anim;

    [SerializeField] bool isAttacking;
    [SerializeField] float damage;
    //public Rigidbody2D body;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayer;

    public override void Enter()
    {
        animator.Play(anim.name);
    }

    // used in an animation event
    void HandleAttack()
    {
        // create a circle collider with the given position, range, and detect with the given layer
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        // do damage to player
        foreach (Collider2D enemy in hits)
        {            
            enemy.GetComponentInChildren<PlayerHealth>().TakeDamage(damage);
            enemy.GetComponent<PlayerMovement>().Knockback(transform);
        }        
    }

    public override void Do()
    {
        // apply more gravity feel to the enemy
        body.velocity = Vector2.down;

        // change state when distance is equal or less than 1.9f
        if (input.DistanceCalculate() > 1.9f)
        {
            isComplete = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}