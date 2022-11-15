using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Declarations
    public int health = 50;
    int currentHealth;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ScoreKeeper scoreKeeper;
    TimerModification timerModification;
    SFXPlayer sfxPlayer;
    [SerializeField] Animator anim;
    #endregion

    private void Awake()
    {
        sfxPlayer = FindObjectOfType<SFXPlayer>();
        timerModification = GameObject.FindGameObjectWithTag("Player").GetComponent<TimerModification>();
        currentHealth = health; //Sets current health equal to maximum health
    }


    public void TakeDamage(int damage)
    {
        CancelInvoke(nameof(OffAnimationHit));
        sfxPlayer.PlayEnemyDamageClip();
        currentHealth -= damage; // Decreases health according to damage taken 
<<<<<<< HEAD

        AnimationController();
=======
        anim.SetBool("Attacking", false);
        anim.SetBool("Hit", true);

        Invoke(nameof(OffAnimationHit),0.5f);
>>>>>>> Animações

        if (currentHealth <= 0)
        {
            Die();
        }
    }

<<<<<<< HEAD
    void AnimationController(){
        anim.SetTrigger("hit");
    }

    void Die()
=======
    private void Die()
>>>>>>> Animações
    {
        currentHealth = 0; // To avoid negative health
        scoreKeeper.ModifyKills(); // If the enemy dies, it will add to the player's score
        timerModification.IncrementOnTime();
    }

    public void RestartHealth() //Will restore the enemy entirely
    {
        currentHealth = health;
        spriteRenderer.color = Color.white;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OffAnimationHit()
    {
        anim.SetBool("Attacking", false);
        anim.SetBool("Hit", false);
    }

}