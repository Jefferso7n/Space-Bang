using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    #region Declarations
    public float thrust = 3;
    public float knockTime = 0.3f;
    [SerializeField] Rigidbody2D rb;
    Rigidbody2D player;
    #endregion

    void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // If the enemy is hit by a bullet, he will be pushed away in the opposite direction of the player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other != null)
            {
                Vector2 difference = rb.transform.position - player.transform.position;
                difference = difference.normalized * thrust;
                rb.AddForce(difference, ForceMode2D.Impulse);

                if (gameObject.activeSelf)
                {
                    StartCoroutine(KnockCo());
                }
            }
        }
    }

    IEnumerator KnockCo()
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
        }
    }
}