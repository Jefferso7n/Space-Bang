using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI statisticsText;
    [SerializeField] ScoreKeeper scoreKeeper;

    void Start()
    {
        statisticsText.text = "DANO CAUSADO: " + scoreKeeper.GetDamage().ToString() +
        "\nINIMIGOS MORTOS: " + scoreKeeper.GetKills().ToString() +
        "\nTEMPO DE JOGO: " + scoreKeeper.GetTimer();
    }

}
