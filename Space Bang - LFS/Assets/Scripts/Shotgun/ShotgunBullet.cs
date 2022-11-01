using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float timeToLive = 0.45f;

    [Header("Score/Damage")]
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] DamageDealer damageDealer;

    private void OnEnable()
    {
        Invoke("Disable", timeToLive);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamagePopup.Create(other.transform.position, damageDealer.GetDamage());
        scoreKeeper.ModifyDamage(damageDealer.GetDamage());

        EnemyHealth EnemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        EnemySpawnPosition EnemySpawnPosition = other.gameObject.GetComponent<EnemySpawnPosition>();
        EnemyHealth.TakeDamage(damageDealer.GetDamage());

        if (EnemyHealth.GetCurrentHealth() <= 0)
        {
            EnemyHealth.RestartHealth();
            EnemySpawnPosition.SpawnInRange(other.gameObject);
            other.gameObject.SetActive(false);
        }
        gameObject.SetActive(false); //Disable bullet
    }
}
