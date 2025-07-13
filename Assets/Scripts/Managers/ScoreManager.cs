using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int points;

    public static ScoreManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        points = 0;
    }
    public void AddPoints(int quantity)
    {
        points += quantity;
    }
}
