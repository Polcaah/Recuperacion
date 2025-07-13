using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] HUD hud;
    [SerializeField] Mario mario;

    bool isRespawning;

    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void OutOfTime()
    {
        if (!isRespawning)
        {
            mario.Dead();
        }
    }
    public void KillZone()
    {
        if (!isRespawning)
        {
            AudioManager.Instance.PlayDie();
            mario.Dead();
        }
    }
}
