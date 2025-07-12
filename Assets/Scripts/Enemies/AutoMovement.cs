using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    private void FixedUpdate()
    {
        if (rb.velocity.x > -0.1f && rb.velocity.x < 0.1)
        {
            speed -= speed;
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
