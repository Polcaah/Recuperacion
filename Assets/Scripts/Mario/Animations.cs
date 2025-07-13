using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Grounded(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }
    public void Velocity(float velocityX)
    {
        animator.SetFloat("VelocityX", Mathf.Abs(velocityX));
    }
    public void Jumping(bool isJumping)
    {
        animator.SetBool("Jumping", isJumping);
    }
    public void Turning(bool isTurning)
    {
        animator.SetBool("Turning", isTurning);
    }
    public void Dead()
    {
        animator.SetTrigger("Dead");
    }
    public void NewState(int state)
    {
        animator.SetInteger("State", state);
    }
    public void PowerUp()
    {
        animator.SetTrigger("PowerUp");
    }
    public void Hit()
    {
        animator.SetTrigger("Hit");
    }
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }
    public void Invincible(bool activate)
    {
        animator.SetBool("Invincible", activate);
    }
    public void Hurt(bool hurt)
    {
        animator.SetBool("Hurt", hurt);
    }
}
