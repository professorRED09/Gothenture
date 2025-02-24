using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCombat : MonoBehaviour
{
    [SerializeField] bool isAttacking;
    //public Rigidbody2D body;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float timeBtwAttack;
    [SerializeField] float startTimeBtwAttack;
    [SerializeField] float delay;

    void Update()
    {
        if (timeBtwAttack <= 0)
        {            
            StartCoroutine(Attack());
            timeBtwAttack = startTimeBtwAttack;
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
            if (timeBtwAttack <= 0)
            {
                timeBtwAttack = 0;
            }
        }       

    }


    IEnumerator Attack()
    {
        yield return new WaitForSeconds(delay);
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D enemy in hits)
        {
            print("HIT PLAYER");
            enemy.GetComponent<PlayerHealth>().TakeDamage(10.0f);
        }
        timeBtwAttack = startTimeBtwAttack;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }
}
