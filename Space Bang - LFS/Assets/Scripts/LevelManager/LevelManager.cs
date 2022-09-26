using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Declarations
    [SerializeField] float sceneLoadDelay = 1f;
    [SerializeField] ScoreKeeper scoreKeeper;
    static LevelManager instance;
    #endregion

    #region Singleton
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Loads
    public void LoadGame()
    {
        Time.timeScale = 1f;
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    }

    public void UnloadSettingsMenu()
    {
        SceneManager.UnloadSceneAsync("SettingsMenu");
    }

    public void LoadGameOverInstantly()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }
    #endregion

    #region IEnumerator of LoadGameOver and QuitGame
    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
