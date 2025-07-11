using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    PlayerMovement player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded", player.isGrounded);
        animator.SetFloat("VelocityX", Mathf.Abs(player.rb.velocity.x));
        animator.SetBool("Jumping", player.isJumping);
        animator.SetBool("Turning", player.isTurning);
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
}
