using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
//   public float currentHealth { get; private set; } = 5f;
    public float currentHealth = 5f;

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
//            currentHealth = 0f;
            Invoke("LoadGameOver", 0f); //Carregar cena depois de X segundos, depois ajustar isso com a animação de morte
//            currentHealth = maxHealth;
        }
    }

    void LoadGameOver(){
        SceneManager.LoadScene("GameOverScreen");
    }
}
