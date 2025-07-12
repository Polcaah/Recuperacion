using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    enum Direction { Left = -1, None = 0, Right = 1 }
    Direction currentDirection = Direction.None;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float maxVelocity;
    [SerializeField] float friction;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float maxJumpingTime = 1f;
    private float jumpTimer = 0;
    [SerializeField] bool isJumping;
    float defaultGravity;

    [Header("State Flags")]
    [SerializeField] bool isTurning;

    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    Collisions collisions;
    Animations animations;

    bool inputMoveEnabled = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();

        defaultGravity = rb.gravityScale;
    }

    private void Update()
    {
        bool grounded = collisions.Grounded();
        animations.Grounded(grounded);

        currentDirection = Direction.None;
        if (inputMoveEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump();
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                currentDirection = Direction.Left;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                currentDirection = Direction.Right;
            }
        }
        if (isJumping)
        {
            if (rb.velocity.y > 0f && Input.GetKey(KeyCode.Space))
            {
                jumpTimer += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space) && jumpTimer < maxJumpingTime)
            {
                rb.gravityScale = defaultGravity * 3f;
            }
            if (rb.velocity.y <= 0f)
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
    }
    private void FixedUpdate()
    {
        isTurning = false;
        float currentVelocity = rb.velocity.x;

        if (currentDirection > 0)
        {
            if (currentVelocity < 0)
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
                currentVelocity -= friction * Time.deltaTime;
            else if (currentVelocity < -1f)
                currentVelocity += friction * Time.deltaTime;
            else
                currentVelocity = 0f;
        }

        rb.velocity = new Vector2(currentVelocity, rb.velocity.y);

        animations.Velocity(currentVelocity);
        animations.Turning(isTurning);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animations.Jumping(true);
        }
    }
    public void Dead()
    {
        inputMoveEnabled = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        rb.AddForce(Vector2.up*5, ForceMode2D.Impulse);
    }
    public void BounceUp()
    {
        rb.velocity = Vector2.zero;
        //Vector2 forceUp = new Vector2(0, 10f);
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
}