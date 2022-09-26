using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI minute1, minute2, colon, second1, second2;
    public float timeDuration = 15f, timer, timerUP, flashTimer, flashDuration = 1f;

    [SerializeField]
    private bool countDown = true;
    [SerializeField] LevelManager levelManager;
    [SerializeField] ScoreKeeper scoreKeeper;

    void Start()
    {
        ResetTimer();
    }

    void FixedUpdate()
    {
        timerUP += Time.fixedDeltaTime;

        if (countDown && timer <= 0f)
        {
            levelManager.LoadGameOverInstantly();
        }
        else if (!countDown && timer < timeDuration)
        {
            timer += Time.fixedDeltaTime;
            UpdateTimerDisplay(timer);
        }
        else if (countDown && timer > 10f)
        {
            timer -= Time.fixedDeltaTime;
            UpdateTimerDisplay(timer);
            UpdateGameTimer(timerUP);
        }
        else
        {
            timer -= Time.fixedDeltaTime;
            Flash();
        }
    }

    private void ResetTimer()
    {
        if (countDown)
        {
            timer = timeDuration;
        }
        else
        {
            timer = 0f;
        }
        timerUP = 0f;
        SetTextDisplay(true);
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        minute1.text = currentTime[0].ToString();
        minute2.text = currentTime[1].ToString();
        second1.text = currentTime[2].ToString();
        second2.text = currentTime[3].ToString();
    }

    private void UpdateGameTimer(float time_)
    {
        float minutes_ = Mathf.FloorToInt(time_ / 60);
        float seconds_ = Mathf.FloorToInt(time_ % 60);

        string currentTime_ = string.Format("{00:00}:{1:00}", minutes_, seconds_);
        scoreKeeper.ModifyTimer(currentTime_);
    }

    private void Flash()
    {
        if (countDown && timer < 0f)
        {
            timer = 0f;
        }
        if (!countDown && timer != timeDuration)
        {
            timer = timeDuration;
        }
        UpdateTimerDisplay(timer);
        UpdateGameTimer(timerUP);

        if (flashTimer <= 0f)
        {
            flashTimer = flashDuration;
        }
        else if (flashTimer >= flashDuration / 2f)
        {
            flashTimer -= Time.fixedDeltaTime;
            SetTextDisplay(false);
        }
        else
        {
            flashTimer -= Time.fixedDeltaTime;
            SetTextDisplay(true);
        }

    }

    private void SetTextDisplay(bool enabled)
    {
        minute1.enabled = enabled;
        minute2.enabled = enabled;
        colon.enabled = enabled;
        second1.enabled = enabled;
        second2.enabled = enabled;
    }
}
