using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    enum State { Default=0, Big=1, Fire=2};
    State currentState = State.Default;
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            animations.PowerUp();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            animations.Hit();
        }
    }
    public void Hit()
    {
        if (currentState == State.Default)
        {
            Dead();
        }
        else
        {
            Time.timeScale = 0;
            animations.Hit();
        }
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
    void ChangeState(int newState)
    {
        currentState = (State)newState;
        animations.NewState(newState);
        Time.timeScale = 1;
    }
}
