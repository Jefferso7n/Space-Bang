using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExtraTimer : MonoBehaviour
{
    public TextMeshProUGUI extraTimerText;
    public TimerModification timerModification;

    [Header("TextDisappearTime")]
    [SerializeField] float disappearTimerMAX = 1f;

    public void ShowTimer()
    {
        gameObject.SetActive(true);
        UpdateTimerText();
        StartCoroutine(HideTimer());
    }

    void UpdateTimerText()
    {
        extraTimerText.text = "+" + timerModification.GetTimerToAdd();
    }

    IEnumerator HideTimer()
    {
        yield return new WaitForSeconds(disappearTimerMAX);
        gameObject.SetActive(false);
    }

}
