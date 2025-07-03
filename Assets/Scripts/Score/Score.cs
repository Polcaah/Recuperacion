using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject visualEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            if (visualEffect != null)
                visualEffect.SetActive(true);
            if (GameManager.instance != null)
                GameManager.instance.AddScore(10);
            Destroy(gameObject, 0.5f);
        }
    }
}
