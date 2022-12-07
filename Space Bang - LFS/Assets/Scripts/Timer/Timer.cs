using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    #region Declarations
    [SerializeField]
    private TextMeshProUGUI minute1, minute2, colon, second1, second2;
    public float timeDuration = 15f, timer, timerUP, flashStart = 10f, flashTimer, flashDuration = 1f;

    [SerializeField]
    private bool countDown = true; // By default there will be countDown
    [SerializeField] LevelManager levelManager;
    [SerializeField] ScoreKeeper scoreKeeper;
    public bool isCounting = true;
    #endregion

    #region Timer
    void Start()
    {
        ResetTimer();
    }

    void FixedUpdate()
    {
        if(!isCounting) return;

        timerUP += Time.fixedDeltaTime;

        if (countDown && timer <= 0f) // If time runs out, load GameOver screen
        {
            levelManager.LoadGameOverInstantly();
        }
        else if (!countDown && timer < timeDuration) // If there is no countDown, then the timer will only increase (no gameover).
        {
            timer += Time.fixedDeltaTime;
            UpdateTimerDisplay(timer);
        }
        else if (countDown && timer > flashStart) // if there is more than 10 seconds(flashStart), the timer display will work normally
        {
            timer -= Time.fixedDeltaTime;
            UpdateTimerDisplay(timer);
            UpdateGameTimer(timerUP);
        }
        else // But if there is less than 10 seconds, the timer display will be flashing
        {
            timer -= Time.fixedDeltaTime;
            Flash();
        }
    }

    // Reset timer at Start
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
        SetTextDisplay(true); // Display timer
    }

    public void AddTimer(float timerToAdd){
        timer += timerToAdd;
    }

    // Update timer text
    public void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        // Format timer
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

    // Flash the timer
    private void Flash()
    {
        if (countDown && timer < 0f) // To avoid negative timer
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

    // Display the timer text
    public void SetTextDisplay(bool enabled)
    {
        minute1.enabled = enabled;
        minute2.enabled = enabled;
        colon.enabled = enabled;
        second1.enabled = enabled;
        second2.enabled = enabled;
    }
    #endregion

}
