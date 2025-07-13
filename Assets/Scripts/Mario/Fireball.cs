using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] public float direction;
    [SerializeField] float speed;
    [SerializeField] float bounceForce;

    [SerializeField] GameObject explosionPrefab;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        speed *= direction;
        rb.velocity = new Vector2 (speed, 0);
    }
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime * -45);
        rb.velocity =new Vector2 (speed, rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.HitFireball();
            Explode(collision.GetContact(0).point);
        }
        else
        {
            Vector2 sidePoint = collision.GetContact(0).normal;
            if (sidePoint.x != 0)
            {
                Explode(collision.GetContact(0).point);
            }
            else if (sidePoint.y > 0)
            {
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
            else if (sidePoint.y < 0)
            {
                rb.AddForce(Vector2.down * bounceForce, ForceMode2D.Impulse);
            }
            else
            {
                Explode(collision.GetContact(0).point);
            }
        }
    }
    void Explode(Vector2 point)
    {
        AudioManager.Instance.PlayBump();
        Instantiate(explosionPrefab, point,Quaternion.identity);
        Destroy(gameObject);
    }
}
