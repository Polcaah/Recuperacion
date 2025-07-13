using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [SerializeField] bool flipSprite= true;

    bool hasBeenVisible;

    float timer = 0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        defaultSpeed = Mathf.Abs(speed);
        rb.isKinematic = true;
        movementPause = true;
    }
    void Activate()
    {
        hasBeenVisible = true;
        rb.isKinematic = false;
        rb.velocity = new Vector2(speed, rb.velocity.y);
        movementPause = false;
    }
    private void Update()
    {
        if(spriteRenderer.isVisible && !hasBeenVisible)
        {
            Activate();
        }
    }
    private void FixedUpdate()
    {
        if (!movementPause)
        {
            if (rb.velocity.x > -0.1f && rb.velocity.x < 0.1)
            {
                if(timer > 0.05f)
                {
                    speed = -speed;
                }
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
            }
                rb.velocity = new Vector2(speed, rb.velocity.y);
            if (flipSprite)
            {
                if (rb.velocity.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
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
    public void ChangeDirection()
    {
        speed = -speed;
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
