using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float frameTime = 0.1f;

    //float timer = 0f;
    int animationFrame = 0;

    public bool stop;
    [SerializeField] bool loop = true;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        if (loop)
        {
            while (!stop)
            {
                spriteRenderer.sprite = sprites[animationFrame];
                animationFrame++;
                if (animationFrame >= sprites.Length)
                {
                    animationFrame = 0;
                }
                yield return new WaitForSeconds(frameTime);
            }
        }
        else
        {
            while(animationFrame < sprites.Length)
            {
                spriteRenderer.sprite = sprites[animationFrame];
                animationFrame++;
                yield return new WaitForSeconds(frameTime);
            }
            Destroy(gameObject);
        }
    }
}
