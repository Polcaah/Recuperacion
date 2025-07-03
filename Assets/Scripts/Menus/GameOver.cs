using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button retryButton;
    public Button mainMenu;

    void Start()
    {
        if (retryButton != null)
            retryButton.onClick.AddListener(() => ChangeScene("Level"));

        if (mainMenu != null)
            mainMenu.onClick.AddListener(() => ChangeScene("MainMenu"));

    }

    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
