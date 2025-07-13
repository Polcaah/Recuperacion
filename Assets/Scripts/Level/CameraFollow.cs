using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followAhead = 2.5f;
    [SerializeField] float minPosX;
    [SerializeField] float maxPosX;

    [SerializeField] Transform limitLeft;
    [SerializeField] Transform limitRight;

    [SerializeField] Transform colLeft;
    [SerializeField] Transform colRight;

    float camWidth;
    float lastPos;
    private void Start()
    {
        camWidth = Camera.main.orthographicSize * Camera.main.aspect;
        minPosX = limitLeft.position.x + camWidth;
        maxPosX = limitRight.position.x - camWidth;
        lastPos = minPosX;

        colLeft.position = new Vector2(transform.position.x - camWidth  -0.75f, colLeft.position.y);
        colRight.position = new Vector2(transform.position.x + camWidth +0.75f, colRight.position.y);
    }
    private void Update()
    {
        float newPosX = target.position.x + followAhead;
        newPosX = Mathf.Clamp(newPosX, lastPos, maxPosX);

        float currentPosX = transform.position.x;

        if (currentPosX < newPosX)
        {
            transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
        }
        //lastPos = newPosX;
    }
}
