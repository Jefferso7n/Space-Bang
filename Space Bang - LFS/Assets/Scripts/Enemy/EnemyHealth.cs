using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 40f;
    public float currentHealth;


    [SerializeField] private GameObject floatingTextPrefab;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(float mod)
    {
        ShowDamage(mod.ToString());
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

    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
