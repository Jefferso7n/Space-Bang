using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 40f;
    public float currentHealth;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void UpdateHealth(float mod)
    {
        spriteRenderer.color = Color.red;
        currentHealth += mod;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0f)
        {
            currentHealth = 0f;
        }
    }

    public void RestartHealth(){
        currentHealth = maxHealth;
        spriteRenderer.color = Color.white;
    }
}
