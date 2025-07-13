using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Mushroom, FireFlower, Coin, Life, Star };
public class Item : MonoBehaviour
{
    public ItemType type;
    bool isCatched;
    public int points;
    [SerializeField] Vector2 startVelocity;
    AutoMovement autoMovement;

    [SerializeField] GameObject floatPointPrefab;
    private void Awake()
    {
        autoMovement = GetComponent<AutoMovement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCatched)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isCatched = true;
                collision.gameObject.GetComponent<Mario>().CatchItem(type);
                CatchItems();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (!isCatched)
        {
            Mario mario = collision.gameObject.GetComponent<Mario>();
            if (mario != null)
            {
                isCatched = true;
                mario.CatchItem(type);
                CatchItems();
            }
        }
    }
    public void WaitMove()
    {
        if(autoMovement != null)
        {
            autoMovement.enabled = false;
        }
    }
    public void StartMove()
    {
        if(autoMovement != null)
        {
            autoMovement.enabled = true;
        }
        else 
        {
            if (startVelocity != Vector2.zero)
            {
                GetComponent<Rigidbody2D>().velocity = startVelocity;
            }
        }
    }
    public void HitBelowBlock()
    {
        if (!autoMovement && autoMovement.enabled)
        {
            autoMovement.ChangeDirection();
        }
    }
    void CatchItems()
    {
        ScoreManager.Instance.AddPoints(points);
        GameObject newFloatPoint = Instantiate(floatPointPrefab, transform.position, Quaternion.identity);
        FloatPoints floatPoints =newFloatPoint.GetComponent<FloatPoints>();
        floatPoints.numPoints = points;

        Destroy(gameObject);
    }
}
