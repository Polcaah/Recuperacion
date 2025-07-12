using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    BoxCollider2D col2D;
    Mario mario;
    Move move;
    private void Awake()
    {
        col2D = GetComponent<BoxCollider2D>();
        mario = GetComponent<Mario>();
        move = GetComponent<Move>();
    }

    public bool Grounded()
    {
        Vector2 footLeft = new Vector2(col2D.bounds.center.x - col2D.bounds.extents.x, col2D.bounds.center.y);
        Vector2 footRight = new Vector2(col2D.bounds.center.x + col2D.bounds.extents.x, col2D.bounds.center.y);

        Debug.DrawRay(footLeft, Vector2.down * col2D.bounds.extents.y * 1.5f, Color.magenta);
        Debug.DrawRay(footRight, Vector2.down * col2D.bounds.extents.y * 1.5f, Color.magenta);

        return Physics2D.Raycast(footLeft, Vector2.down, col2D.bounds.extents.y * 1.5f, groundLayer) ||
               Physics2D.Raycast(footRight, Vector2.down, col2D.bounds.extents.y * 1.5f, groundLayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            mario.Hit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*PlayerHit playerHit = collision.GetComponent<PlayerHit>();
        if(playerHit != null)
        {
            playerHit.Hit();
            move.BounceUp();
        }*/
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Stomped(transform);
            move.BounceUp();
        }
    }
    public void Dead()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerDead");
    }
}
