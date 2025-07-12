using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    bool movementPause;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    Vector2 lastVelocity;
    Vector2 currentDirection;
    float defaultSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        defaultSpeed = Mathf.Abs(speed);
    }
    private void FixedUpdate()
    {
        if (!movementPause)
        {
            if (rb.velocity.x > -0.1f && rb.velocity.x < 0.1)
            {
                speed = -speed;
            }
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (rb.velocity.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX= false;
            }
        }
    }
    public void PauseMovement()
    {
        if (!movementPause)
        {
            currentDirection = rb.velocity.normalized;
            lastVelocity = rb.velocity;
            movementPause = true;
            rb.velocity = new Vector2(0, 0);
        }
    }
    public void ContinueMovement()
    {
        if(movementPause)
        {
            speed = defaultSpeed * currentDirection.x;
            rb.velocity = new Vector2(speed, lastVelocity.y);
            movementPause = false;
        }
    }
    public void ContinueMovement(Vector2 newVelocity)
    {
        if (movementPause)
        {
            rb.velocity = newVelocity;
            movementPause = false;
        }
    }
}
