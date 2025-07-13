using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPole : MonoBehaviour
{
    public Transform flag;
    public Transform bottom;
    public float flagVelocity = 5f;

    bool downFlag;
    Move move;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mario mario = collision.GetComponent<Mario>();
        if(mario != null)
        {
            downFlag = true;
            mario.Goal();
            move = collision.GetComponent<Move>();
            Vector2 contactPoint = collision.ClosestPoint(transform.position);
            CalculateHeight(contactPoint.y);
        }
    }
    private void FixedUpdate()
    {
        if (downFlag)
        {
            if(flag.position.y > bottom.position.y)
            {
                flag.position = new Vector2(flag.position.x, flag.position.y - (flagVelocity * Time.deltaTime));
            }
            else
            {
                move.isFlagDown = true;
            }
        }
    }
    void CalculateHeight (float marioPosition)
    {
        float size =GetComponent<BoxCollider2D>().bounds.size.y;
        float minPosition1 = transform.position.y + (size - size/5);
        float minPosition2 = transform.position.y + (size - 2*size / 5);
        float minPosition3 = transform.position.y + (size - 3*size / 5);
        float minPosition4 = transform.position.y + (size - 4*size / 5);
        if(marioPosition >= minPosition1)
        {
            ScoreManager.Instance.AddPoints(5000);
        }
        else if (marioPosition >= minPosition2)
        {
            ScoreManager.Instance.AddPoints(2000);
        }
        else if (marioPosition >= minPosition3)
        {
            ScoreManager.Instance.AddPoints(800);
        }
        else if (marioPosition >= minPosition4)
        {
            ScoreManager.Instance.AddPoints(400);
        }
        else
        {
            ScoreManager.Instance.AddPoints(100);
        }

    }
}
