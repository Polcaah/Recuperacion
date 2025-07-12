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
}
