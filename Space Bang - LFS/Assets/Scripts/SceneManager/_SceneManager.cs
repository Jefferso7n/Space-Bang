using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class _SceneManager : MonoBehaviour
{
    public void LoadScenes(string scene){
        Time.timeScale = 1f;
        PlayerPrefs.SetFloat("totalDamage", 0f);
        PlayerPrefs.SetFloat("enemiesKilled", 0f);
        SceneManager.LoadScene(scene);
    }

    public void LoadSettingsMenu(){
            SceneManager.LoadScene("SettingsMenu", LoadSceneMode.Additive);
    }

    public void UnloadSettingsMenu(){
        SceneManager.UnloadSceneAsync("SettingsMenu");
    }

    public void Quit(){
        Application.Quit();
    }

}
