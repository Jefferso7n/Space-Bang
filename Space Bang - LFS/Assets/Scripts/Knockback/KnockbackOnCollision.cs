using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnCollision : MonoBehaviour
{
    #region Declarations
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float knockbackStrength = 7.5f;
    [SerializeField] float knockbackTime = 0.25f;
    [SerializeField] IFrame iFrame;
    Vector2 direction;
    public bool isInKnockback = false;
    bool canBeKnockback = true;
    #endregion

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && canBeKnockback)
        {
            canBeKnockback = false;
            isInKnockback = true;

            direction = (transform.position - other.transform.position).normalized;
            direction = direction * knockbackStrength;

            playerRb.AddForce(direction, ForceMode2D.Impulse);
            StartCoroutine(CoKnock());
        }
    }

    IEnumerator CoKnock()
    {
        yield return new WaitForSeconds(knockbackTime); //After this the player can move on PlayerController script
        playerRb.velocity = Vector2.zero;
        isInKnockback = false;

        yield return new WaitForSeconds(iFrame.GetDuration() - knockbackTime); //After this the player can be affected again by knockback
        canBeKnockback = true;
    }

}