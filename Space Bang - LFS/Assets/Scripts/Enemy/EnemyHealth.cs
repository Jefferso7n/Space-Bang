using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health = 50;
    public float currentHealth;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ScoreKeeper scoreKeeper;

    private void Awake()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentHealth = 0;
        scoreKeeper.ModifyKills();
    }

    public void RestartHealth()
    {
        currentHealth = health;
        spriteRenderer.color = Color.white;
    }
}
