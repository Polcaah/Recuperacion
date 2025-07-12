using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private GameObject deathEffect;

    private bool isDead = false;
    private bool movingLeft = true;

    private void Awake()
    {
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void Update()
    {
        float direction = movingLeft ? -1f : 1f;
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("Player"))
        {
            if (!collision.collider.GetComponentInChildren<EnemyHead>())
            {
                collision.collider.GetComponent<Health>()?.TakeDamage(damage);
            }
        }
        else
        {
            movingLeft = !movingLeft;
            Flip();
        }
    }

    public void KillEnemy()
    {
        if (isDead) return;
        isDead = true;

        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
