using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    [Header("Scenes")]
    public string GameSceneName;
    public string MainMenuName;

    #region Init
    private void Awake()
    {
        if (instance == null || instance != this)
            instance = this;
    }
    #endregion

    public void LoadMainMenu()
    {
        LoadScene(MainMenuName);
    }

    public void LoadGameScene()
    {
        LoadScene(GameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
