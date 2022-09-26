using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerHealth playerHealth;

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
    }
}