using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    void OnEnable(){
        Cursor.visible = true;
    }

    public void Setup(int score){
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " PONTOS";
    }

}
