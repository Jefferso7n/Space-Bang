using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image circleBar;
    [SerializeField] Image extraBar;

    [SerializeField] PlayerHealth playerHealth;

    public float circlePercentage = 0.4f; //How much of the whole healthBar is the circular part
    public float circleFillAmount = 0.738f; //How much of the circular part is used in the healthbar

    void FixedUpdate()
    {
        CircleFill();
        ExtraFill();
    }

    private void CircleFill()
    {
        float healthPercentage =  (float) playerHealth.GetHealth() / playerHealth.GetMaxHealth();
        float circleFill = healthPercentage / circlePercentage;

        circleFill *= circleFillAmount;
        circleFill = Mathf.Clamp(circleFill, 0, circleFillAmount);

        circleBar.fillAmount = circleFill;
    }

    void ExtraFill()
    {
        float circleAmount = circlePercentage * playerHealth.GetMaxHealth();

        float extraHealth = playerHealth.GetHealth() - circleAmount;
        float extraTotalHealth = playerHealth.GetMaxHealth() - circleAmount;

        float extraFill = extraHealth / extraTotalHealth;
        extraFill = Mathf.Clamp(extraFill, 0, 1);

        extraBar.fillAmount = extraFill;
    }
}
