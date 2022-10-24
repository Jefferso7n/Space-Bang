using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    #region Declarations
    [SerializeField] float speed = 1.75f;
    [SerializeField] float attackSpeed = 2.5f;
    private float canAttack;

    DamageDealer damageDealer;
    Transform target;
    [SerializeField] Rigidbody2D rb;
    private Vector2 movement;
    #endregion

    void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
    }

    #region Movement
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //Gets the direction to the player (target)
        Vector3 direction = target.position - transform.position;

        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveEnemy(movement);
    }

    private void MoveEnemy(Vector2 direction)
    {
        // Moves the enemy to the player (target)
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public float GetSpeed(){
        return speed;
    }

    public void setSpeed(float newSpeed){
        speed = newSpeed;
    }
    #endregion

    #region Collisions
    //The player loses life when colliding with an enemy
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth.IsAlive())
            {
//                playerHealth.HitEffect();
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
//                    playerHealth.HitEffect();
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
    #endregion
}
