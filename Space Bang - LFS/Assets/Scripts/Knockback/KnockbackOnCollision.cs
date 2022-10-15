using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnCollision : MonoBehaviour
{
    #region Declarations
    [SerializeField] float knockbackStrength = 10f;
    #endregion

    // If the enemy is hit by a bullet, he will be pushed away
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Rigidbody2D rb = collision2D.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = collision2D.transform.position - transform.position;
            direction.y = 0;
            Debug.Log(direction);

            rb.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
        }
    }
}