using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string levelSceneName = "Level";
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        if (retryButton != null)
            retryButton.onClick.AddListener(() => ChangeScene(levelSceneName));

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(() => ChangeScene(mainMenuSceneName));
    }

    private void ChangeScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned in GameOver script.");
        }
    }
}
