using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public GameObject stompBox;
    Move move;
    Collisions collisions;
    Animations animations;
    Rigidbody2D rb;

    bool isDead;
    private void Awake()
    {
        move = GetComponent<Move>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (rb.velocity.y < 0 && !isDead)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }
    public void Hit()
    {
        Dead();
    }
    public void Dead()
    {
        if (!isDead)
        {
            isDead = true;
            move.Dead();
            collisions.Dead();
            animations.Dead();
        }
    }
}
