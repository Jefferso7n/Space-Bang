using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerModification : MonoBehaviour
{
    [Header("Kills")]
    [SerializeField] ScoreKeeper scoreKeeper;

    [Header("Timer")]
    [SerializeField] Timer timer;
    public int killsNecessaryToIncrementTime = 2;
    [SerializeField] float timerToAdd = 10f;

    [Header("UI")]
    [SerializeField] ExtraTimer extraTimer;
    [SerializeField] KillsUI killsUI;

    public void IncrementOnTime(){
        if (scoreKeeper.GetKills() % killsNecessaryToIncrementTime == 0){
//            Debug.Log("Kills: " + scoreKeeper.GetKills());
            timer.AddTimer(timerToAdd);
            timer.SetTextDisplay(true);
            extraTimer.ShowTimer();
        }
            killsUI.UpdateKillsText();
    }

    public float GetTimerToAdd(){
        return timerToAdd;
    }

}
