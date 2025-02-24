using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    //public float range;
    public int damage = 10;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    //private Transform target; // The current enemy the player is targeting
    //private Vector2 direction;

    private void Start()
    {
        //FindNearestEnemy();
        rb = GetComponent<Rigidbody2D>();
    }

    //private void Update()
    //{
    //    //if(target!= null)
    //    //{
    //    //    Vector2 direction = (target.position - transform.position).normalized;
    //    //    rb.velocity = direction * speed * Time.deltaTime;
    //    //}

    //    Vector2 direction = (target.position - transform.position).normalized;
    //    rb.velocity = direction * speed * Time.deltaTime;

    //}

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.GetComponentInChildren<IHealth>() != null)
        {
            // Assume the player has a script with a TakeDamage method.
            enemy.GetComponent<IHealth>().TakeDamage(10.0f);
            Destroy(gameObject); // Destroy the fireball after hitting the player.
        }
        else if (((1 << enemy.gameObject.layer) & groundLayer) != 0)
        {
            print("HIT GROUND");
            Destroy(gameObject);
        }
    }

    //void FindNearestEnemy()
    //{
    //    // Find all enemies within the aim range
    //    Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

    //    float closestDistance = Mathf.Infinity;
    //    Transform nearestEnemy = null;

    //    foreach (Collider2D enemy in enemiesInRange)
    //    {
    //        float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
    //        if (distanceToEnemy < closestDistance)
    //        {
    //            closestDistance = distanceToEnemy;
    //            nearestEnemy = enemy.transform;
    //        }
    //    }

    //    target = nearestEnemy; // Update the target enemy
    //}

    //void AimAtTarget()
    //{
    //    Vector2 direction = (target.position - transform.position).normalized; // Direction to the target
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;           // Convert direction to angle
    //    transform.rotation = Quaternion.Euler(0, 0, angle);                         // Rotate the weapon
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);     
        
    //}
}
