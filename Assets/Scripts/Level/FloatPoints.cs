using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Unity.VisualScripting;

public class FloatPoints : MonoBehaviour
{
    [SerializeField] public int numPoints = 0;
    [SerializeField] public float distance = 2f;
    [SerializeField] public float speed = 2f;
    [SerializeField] public bool destroy = true;

    float targetPos;

    [SerializeField] Points[] points;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Show(numPoints);
        targetPos = transform.position.y + distance;
    }
    private void Update()
    {
        if(transform.position.y < targetPos)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime));
        }
        else if (destroy)
        {
            Destroy(gameObject);
        }
    }
    void Show(int points)
    {
        for (int i = 0; i < this.points.Length; i++)
        {
            if (this.points[i].numPoints  == points)
            {
                spriteRenderer.sprite  = this.points[i].sprite;
                break;
            }
        }
        
    }
}
[Serializable]
public class Points
{
    public int numPoints;
    public Sprite sprite;
}