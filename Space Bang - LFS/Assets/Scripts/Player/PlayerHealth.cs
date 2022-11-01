using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Declarations
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool applyCameraShake;
    [SerializeField] IFrame iFrame;

    CameraShake cameraShake;
    SFXPlayer sfxPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    int health;
    bool isAlive = true;
    #endregion

    void Awake()
    {
        health = maxHealth;
        cameraShake = Camera.main.GetComponent<CameraShake>();
        sfxPlayer = FindObjectOfType<SFXPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    #region Health
    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    #endregion

    #region Damage
    public void TakeDamage(int damage)
    {
        if (iFrame.canBeHurt)
        {
            sfxPlayer.PlayPlayerDamageClip();
            iFrame.StartCoroutine(iFrame.CoFlash());
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        health = 0;
        isAlive = false;
        levelManager.LoadGameOverInstantly();
//        levelManager.LoadGameOver();
    }
    #endregion
}
