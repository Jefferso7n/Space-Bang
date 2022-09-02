using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class _SceneManager : MonoBehaviour
{
    public void LoadScenes(string scene){
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void Quit(){
        Application.Quit();
    }

}
