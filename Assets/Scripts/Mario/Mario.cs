using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    enum State { Default = 0, Big = 1, Fire = 2 };
    State currentState = State.Default;
    [SerializeField] GameObject stompBox;
    [SerializeField] GameObject headBox;
    Move move;
    Collisions collisions;
    Animations animations;
    Rigidbody2D rb;

    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform shootPos;

    [SerializeField] public bool isInvincible;
    [SerializeField] float invincibleTime;
    float invincibleTimer;

    [SerializeField] bool isHurt;
    [SerializeField] float hurtTime;
    float hurtTimer;
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
        if (!isDead)
        {
            if (rb.velocity.y < 0 && !isDead)
            {
                stompBox.SetActive(true);
            }
            else
            {
                stompBox.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Shoot();
            }
            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer <= 0)
                {
                    isInvincible = false;
                    animations.Invincible(false);
                }
            }
            if (isHurt)
            {
                hurtTimer -= Time.deltaTime;
                if(hurtTime <= 0)
                {
                    EndHurt();
                }
            }
        }
        if (rb.velocity.y > 0)
        {
            headBox.SetActive(true);
        }
        else
        {
            headBox.SetActive(false);
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
        if (!isHurt)
        {
            if (currentState == State.Default)
            {
                Dead();
            }
            else
            {
                Time.timeScale = 0;
                animations.Hit();
                StartHurt();
            }
        }
    }
    void StartHurt()
    {
        isHurt = true;
        animations.Hurt(true);
        hurtTimer = hurtTime;
        collisions.HurtCollision(true);
    }
    void EndHurt()
    {
        isHurt = false;
        animations.Hurt(false);
        collisions.HurtCollision(false);
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
    public void CatchItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Mushroom:
                if (currentState == State.Default)
                {
                    animations.PowerUp();
                    Time.timeScale = 0;
                }
                else
                {

                }
                break;
            case ItemType.FireFlower:
                if (currentState != State.Fire)
                {
                    animations.PowerUp();
                    Time.timeScale = 0;
                }
                else
                {

                }
                break;
            case ItemType.Coin:
                Debug.Log("Coin Collected");
                break;
            case ItemType.Life:

                break;
            case ItemType.Star:
                isInvincible = true;
                animations.Invincible(true);
                invincibleTimer = invincibleTime;
                EndHurt();
                break;
        }
    }
    void Shoot()
    {
        if (currentState == State.Fire)
        {
            GameObject newfireBall = Instantiate(fireballPrefab, shootPos.position, Quaternion.identity);
            newfireBall.GetComponent<Fireball>().direction = transform.localScale.x;
            animations.Shoot();
        }
    }
    public bool IsBig()
    {
        return currentState != State.Default;
    }
}