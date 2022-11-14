using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillsUI : MonoBehaviour
{
    [Header("Kills")]
    public TextMeshProUGUI killsText;
    int kills;
    int maxKills;

    [Header("Timer")]
    [SerializeField] TimerModification timerModification;

    void Start() {
        kills = 0;
        maxKills = timerModification.killsNecessaryToIncrementTime;

        killsText.text = "KILLS: " + kills + " / " + maxKills;
    }

    public void UpdateKillsText(){
        if (kills >= maxKills){
            kills = 0;
        }
        kills++;

        killsText.text = "KILLS: " + kills + "/" + maxKills;
    }

}
