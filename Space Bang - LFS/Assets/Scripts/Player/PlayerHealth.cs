using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    bool isAlive = true;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Effects to occur when the player collides with the enemy
    public void HitEffect(){
        ShakeCamera();
        audioPlayer.PlayDamageClip();
    }

    public int GetHealth()
    {
        return health;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        health = 0;
        isAlive = false;
        levelManager.LoadGameOver();
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
