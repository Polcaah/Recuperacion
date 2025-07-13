using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI numCoins;
    public TextMeshProUGUI time;

    private void Update()
    {
        score.text = ScoreManager.Instance.points.ToString("D6");
    }
    public void UpdateCoins(int totalCoins)
    {
        numCoins.text = totalCoins.ToString("D2");
    }
    public void UpdateTime(float timeLeft)
    {
        int timeLeftInt = Mathf.RoundToInt(timeLeft);
        time.text = timeLeftInt.ToString("D3");
    }
}
