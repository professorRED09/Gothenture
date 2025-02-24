using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public LayerMask groundLayer;

    private Vector2 direction;

    public void Initialize(Vector2 targetPosition)
    {
        // Calculate the direction towards the target position.
        direction = (targetPosition - (Vector2)transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void CastFireBall()
    {
        // Move the fireball in the specified direction.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void GotParried()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<PlayerHealth>() != null)
        {
            // Assume the player has a script with a TakeDamage method.
            collision.GetComponentInChildren<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destroy the fireball after hitting the player.
        }
        else if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            print("HIT GROUND");
            Destroy(gameObject);
        }
    }
}
