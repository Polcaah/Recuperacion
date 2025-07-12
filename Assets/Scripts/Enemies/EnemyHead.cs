using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    [SerializeField] private Goomba goomba;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        bool isFalling = rb.velocity.y <= 0;

        bool isAbove = collision.transform.position.y > transform.position.y;

        if (isAbove && isFalling)
        {
            goomba.KillEnemy();

            rb.velocity = new Vector2(rb.velocity.x, 8f);
        }
        else
        {
            collision.GetComponent<Move>()?.TakeDamage(goomba.GetDamage());
        }
    }
}