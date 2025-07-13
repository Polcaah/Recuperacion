using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] bool isBreakable;
    [SerializeField] GameObject brickPiecePrefab;

    [SerializeField] int numCoins;
    [SerializeField] GameObject coinBlockPrefab;

    bool bouncing;

    [SerializeField] Sprite emptyBlock;
    bool isEmpty;

    [SerializeField] GameObject itemPrefab;

    public LayerMask onBlockLayers;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void OnTheBlock()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center + Vector3.up*boxCollider.bounds.extents.y, boxCollider.bounds.size*0.5f, 0, onBlockLayers);
        foreach(Collider2D c in colliders)
        {
            Enemy enemy = c.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.HitBelowBlock();
            }
            else
            {
                Item item = c.GetComponent<Item>();
                if (item != null)
                {
                    item.HitBelowBlock();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center + Vector3.up * boxCollider.bounds.extents.y, boxCollider.bounds.size * 0.5f);
        }
    }
    public void HeadCollision(bool marioBig)
    {
        if (isBreakable)
        {
            if (marioBig)
            {
                Break();
            }
            else
            {
                Bounce();
            }
        }
        else if (!isEmpty)
        {
            if (numCoins > 0)
            {
                if (!bouncing)
                {
                    Instantiate(coinBlockPrefab, transform.position, Quaternion.identity);
                    numCoins--;
                    Bounce();
                    if (numCoins <= 0)
                    {
                        isEmpty = true;
                    }
                }
            }
            else if (itemPrefab != null)
            {
                if (!bouncing)
                {
                    StartCoroutine(ShowItem());
                    Bounce();
                    isEmpty = true;
                }
            }
        }
        if (!isEmpty)
        {
            OnTheBlock();
        }
    }
    void Bounce()
    {
        if(!bouncing)
        {
            StartCoroutine(BounceAnimation());
        }
    }
    IEnumerator BounceAnimation()
    {
        bouncing = true;
        float time = 0;
        float duration = 0.1f;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = (Vector2)transform.position + Vector2.up * 0.15f;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        time = 0;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(targetPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;
        bouncing = false;
        if (isEmpty)
        {
            SpritesAnimation spritesAnimation =GetComponent<SpritesAnimation>();
            if (spritesAnimation != null)
            {
                spritesAnimation.stop = true;
            }
            GetComponent<SpriteRenderer>().sprite = emptyBlock;
        }
    }
    void Break()
    {
        GameObject brickPiece;
        //Up-Right
        brickPiece =Instantiate(brickPiecePrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
        brickPiece.GetComponent<Rigidbody2D>().velocity= new Vector2(3f, 8f);
        //Up-Left
        brickPiece = Instantiate(brickPiecePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        brickPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 7f);
        //Down-Right
        brickPiece = Instantiate(brickPiecePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
        brickPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, -4f);
        //Down-Left
        brickPiece = Instantiate(brickPiecePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        brickPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, -5f);

        Destroy(gameObject);
    }
    IEnumerator ShowItem()
    {
        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity); 
        //AutoMovement autoMovement =newItem.GetComponent<AutoMovement>();
        //if (autoMovement != null)
        //{
        //    autoMovement.enabled = false;
        //}
        Item item = newItem.GetComponent<Item>();
        item.WaitMove();

        float time = 0;
        float duration = 1f;

        Vector2 startPosition = newItem.transform.position;
        Vector2 targetPosition = (Vector2)transform.position + Vector2.up * 0.5f;

        while (time < duration)
        {
            newItem.transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        newItem.transform.position = targetPosition;
        //if(autoMovement != null)
        //{
        //    autoMovement.enabled = true;
        //}
        item.StartMove();
    }
}
