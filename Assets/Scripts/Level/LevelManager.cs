using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public HUD hud;
    int coins;

    [SerializeField] int time;
    [SerializeField] float timer;
    public static LevelManager Instance;

    Mario mario;

    public bool levelFinished;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        coins = 0;
        hud.UpdateCoins(coins);

        timer = time;
        hud.UpdateTime(timer);
        mario = GetComponent<Mario>();
    }
    private void Update()
    {
        if (!levelFinished)
        {
            timer -= Time.deltaTime / 03f;
            hud.UpdateTime(timer);
            if (timer <= 0)
            {
                mario.Dead();
                timer = 0;
            }
        }
    }
    public void AddCoins()
    {
        coins++;
        hud.UpdateCoins(coins);
    }
    public void LevelFinished()
    {
        AudioManager.Instance.FinishLevel();
        levelFinished = true;
        StartCoroutine(SecondsToPoints());
    }
    IEnumerator SecondsToPoints()
    {
        yield return new WaitForSeconds(1f);
        
        int timeLeft =Mathf.RoundToInt(timer);
        while(timeLeft > 0)
        {
            timeLeft--;
            hud.UpdateTime(timeLeft);
            ScoreManager.Instance.AddPoints(50);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
