using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackAndRecoil : MonoBehaviour
{
    #region Declarations
    [Header("Player")]
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] IFrame iFrame;

    [Header("Knockback")]
    [SerializeField] float knockbackStrength = 7.5f;
    [SerializeField] float knockbackTime = 0.25f;
    public bool isInKnockback = false;
    bool canBeKnockback = true;

    [Header("Shotgun Recoil")]
    [SerializeField] Shotgun shotgun;
    [SerializeField] float recoilStrength = 3f;
    [SerializeField] float recoilTime = 0.25f;

    Vector2 direction;
    #endregion

    public void ShotgunRecoil()
    {
        if (canBeKnockback)
        {
            canBeKnockback = false;
            isInKnockback = true;

            direction = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            AdjustKnockbackStrength();

            playerRb.AddForce(direction, ForceMode2D.Impulse);
            StartCoroutine(CoRecoil());
        }
    }

    void AdjustKnockbackStrength(){
        direction.x = Mathf.Sign(direction.x) * recoilStrength;
        direction.y = Mathf.Sign(direction.y) * recoilStrength;
    }

    //When the player gets hurt by a enemy
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

    IEnumerator CoRecoil()
    {
        yield return new WaitForSeconds(recoilTime); //After this the player can move on PlayerController script
        playerRb.velocity = Vector2.zero;
        isInKnockback = false;

        yield return new WaitForSeconds(shotgun.timeBetweenFiring - recoilTime); //After this the player can be affected again by the recoil
        canBeKnockback = true;
    }

}