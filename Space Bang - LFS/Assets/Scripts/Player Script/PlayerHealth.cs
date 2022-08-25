using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; } = 5f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float mod)
    {
        currentHealth += mod;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } else if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
    }
}
