using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI statisticsText;

    void Start(){
        statisticsText.text = "DANO CAUSADO: " + PlayerPrefs.GetFloat("totalDamage").ToString() +
        "\nINIMIGOS MORTOS: " + PlayerPrefs.GetFloat("enemiesKilled").ToString();
    }

}
