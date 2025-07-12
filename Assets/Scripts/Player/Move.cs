using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    enum Direction { Left = -1, None = 0, Right = 1 }
    Direction currentDirection= Direction.None;

    [SerializeField] float speed;
    [SerializeField]float acceleration;
    [SerializeField]float maxVelocity;
    [SerializeField] float friction;
    private float currentVelocity = 0f;

    [SerializeField] float jumpForce;
    [SerializeField] float maxJumpingTime = 1f;
    [SerializeField] bool isJumping;
    private float jumpTimer = 0;
    private float defaultGravity = 0f;

    [SerializeField]bool isTurning;

    [SerializeField]Rigidbody2D rb;
    Collisions collisions;

    public bool inputMoveEnabled = true;

    Animations animations;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();
    }
    private void Start()
    {
        defaultGravity = rb.gravityScale;
    }
    void Update()
    {
        bool grounded = collisions.Grounded();
        animations.Grounded(grounded);
        if (isJumping)
        {
            /*if(rb.velocity.y < 0f)
            {
                rb.gravityScale = defaultGravity;
                if (grounded)
                {
                    isJumping = false;
                    jumpTimer = 0;
                    animations.Jumping(false);  
                }
            }*/
            if (rb.velocity.y > 0f)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (jumpTimer < maxJumpingTime)
                    {
                        rb.gravityScale = defaultGravity * 3f;
                    }
                }
            }
            else
            {
                rb.gravityScale = defaultGravity;
                if (grounded)
                {
                    isJumping = false;
                    jumpTimer = 0;
                    animations.Jumping(false);
                }
            }
        }
        currentDirection = Direction.None;
        if (inputMoveEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    Jump();
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentDirection = Direction.Left;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentDirection = Direction.Right;
            }
        }
    }
    private void FixedUpdate()
    {
        isTurning = false;
        currentVelocity = rb.velocity.x;
        if (currentDirection > 0)
        {
            if(currentVelocity < 0)
            {
                currentVelocity += (acceleration + friction) * Time.deltaTime;
                isTurning = true;
            }
            else if (currentVelocity < maxVelocity)
            {
                currentVelocity += acceleration * Time.deltaTime;
                transform.localScale = new Vector2(1, 1);
            }
        }
        else if (currentDirection < 0)
        {
            if (currentVelocity > 0)
            {
                currentVelocity -= (acceleration + friction) * Time.deltaTime;
                isTurning = true;
            }
            else if (currentVelocity > -maxVelocity)
            {
                currentVelocity -= acceleration * Time.deltaTime;
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            if (currentVelocity > 1f)
            {
                currentVelocity -= friction * Time.deltaTime;
            }
            else if (currentVelocity < -1f)
            {
                currentVelocity += friction * Time.deltaTime;
            }
            else
            {
                currentVelocity = 0;
            }
        }
        Vector2 velocity = new Vector2(currentVelocity, rb.velocity.y);
        rb.velocity = velocity;

        animations.Velocity(currentVelocity);
        animations.Turning(isTurning);
    }
    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            Vector2 force = new Vector2(0, jumpForce);
            rb.AddForce(force, ForceMode2D.Impulse);
            animations.Jumping(true);
        }
    }
    public void Dead()
    {
        inputMoveEnabled = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        rb.AddForce(Vector2.up*5f,ForceMode2D.Impulse);
    }
    public void BounceUp()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
    void MoveRight()
    {
        Vector2 velocity = new Vector2 (1f, 0f);
        rb.velocity = velocity;
    }
}
