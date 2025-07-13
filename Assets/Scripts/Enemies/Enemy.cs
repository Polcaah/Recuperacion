using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int points;
    protected Animator animator;
    protected AutoMovement autoMovement;
    protected Rigidbody2D rb;

    [SerializeField] GameObject floatPointPrefab;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        autoMovement = GetComponent<AutoMovement>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {

    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            autoMovement.ChangeDirection();
        }
    }
    public virtual void Stomped(Transform player)
    {

    }
    public virtual void HitFireball()
    {
        FlipDie();
    }
    public virtual void HitStarMan()
    {
        FlipDie();
    }
    public virtual void HitBelowBlock()
    {
        FlipDie();
    }
    public virtual void HitRollingShell()
    {
        FlipDie();
    }
    protected void FlipDie()
    {
        AudioManager.Instance.PlayFlipDie();
        animator.SetTrigger("Flip");
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        if (autoMovement != null)
        {
            animator.enabled = true;
        }
        GetComponent<Collider2D>().enabled = false;
        Dead();
    }
    protected void Dead()
    {
        GameObject newFloatPoint = Instantiate(floatPointPrefab, transform.position, Quaternion.identity);
        FloatPoints floatPoints = newFloatPoint.GetComponent<FloatPoints>();
        floatPoints.numPoints = points;
    }
}
