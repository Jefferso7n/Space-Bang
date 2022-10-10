using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    #region Declarations
    public float speed { get; private set; } = 2.25f;
    public float respeed { get; private set; }
    public float attackSpeed { get; private set; } = 2.5f;
    public float canAttack;
    //    public AnimationCurve a;

    DamageDealer damageDealer;
    Transform target;
    [SerializeField] Rigidbody2D rb;
    Vector3 distance;
    #endregion

    void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
    }

    void Start()
    {
        respeed = speed;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // Moves the enemy to the player (target)
        distance = target.position - transform.position;
        distance = distance.normalized;
        distance = distance * speed;

        rb.AddForce(distance);
        //        a.Evaluate();
    }

    //The player loses life when colliding with an enemy
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth.IsAlive())
            {
                playerHealth.HitEffect();
                playerHealth.TakeDamage(damageDealer.GetDamage());
            }

            canAttack = 0f;
        }
    }

    //The player continues to lose health if he is leaning against the enemy
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            // After a while, if the enemy attack is not on cooldown, the player loses life. (Based on enemy attack speed)
            if (attackSpeed <= canAttack)
            {
                if (playerHealth.IsAlive())
                {
                    playerHealth.HitEffect();
                    playerHealth.TakeDamage(damageDealer.GetDamage());
                }

                canAttack = 0f; // Reset cooldown
            }
            else
            {
                canAttack += Time.fixedDeltaTime;
            }
        }
    }
}
