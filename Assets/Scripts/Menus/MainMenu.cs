using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;
    public Button exitButton;

    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(() => ChangeScene("Level"));

        if (settingsButton != null)
            settingsButton.onClick.AddListener(() => ChangeScene("Settings"));

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }

    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
