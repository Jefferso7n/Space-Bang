using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Declarations
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;
    #endregion

    void Start()
    {
        GameIsPaused = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount == 1)
        // Added sceneCount condition to avoid conflicts with Settings Menu screen
        {
            // Player can enable/disable pause by pressing Escape key
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    #region Resume / Pause
    // When resumed, the pause menu disappears and time returns to normal scale.
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // When paused, time is stopped and the options menu opens
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    #endregion

}
