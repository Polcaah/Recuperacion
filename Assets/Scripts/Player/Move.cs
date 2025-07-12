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

    [Header("Health")]
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private int startingLives = 3;
    private float currentHealth;
    private int currentLives;
    private bool dead = false;
    private Vector3 spawnPosition;

    [Header("State Flags")]
    [SerializeField] bool isTurning;

    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    Collisions collisions;
    Animations animations;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();

        defaultGravity = rb.gravityScale;
        currentHealth = startingHealth;
        currentLives = startingLives;
        spawnPosition = transform.position;
    }

    private void Update()
    {
        bool grounded = collisions.Grounded();
        animations.Grounded(grounded);

        HandleJumping(grounded);
        HandleInput(grounded);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleInput(bool grounded)
    {
        currentDirection = Direction.None;

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

    private void HandleJumping(bool grounded)
    {
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

    private void ApplyMovement()
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

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            currentLives--;
            enabled = false;

            Invoke(nameof(Respawn), 0.3f);
        }
        Debug.Log(currentHealth);
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    private void Respawn()
    {
        if (currentLives > 0)
        {
            transform.position = spawnPosition;
            currentHealth = startingHealth;
            dead = false;
            enabled = true;

            if (GameManager.instance != null)
                GameManager.instance.AddScore(-20);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public int GetLives() => currentLives;
}