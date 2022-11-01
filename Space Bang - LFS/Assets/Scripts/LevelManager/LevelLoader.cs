using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

    public void LoadGame()
    {
        Time.timeScale = 1f; // To ensure time is not paused when starting the game
        scoreKeeper.ResetScore();
        StartCoroutine(LoadAsynchronously("Game"));
    }
    IEnumerator LoadAsynchronously (string scene_Name){
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_Name);

        loadingScreen.SetActive(true);


        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;

            yield return null;
        }
    }
}
