using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFrame : MonoBehaviour
{
    [Header("Invulnerability")]
    [SerializeField] Color flashColor;
    [SerializeField] Color regularColor;
    [SerializeField] float flashDuration;
    [SerializeField] int numberOfFlashes;
    [SerializeField] SpriteRenderer mySprite;
    [HideInInspector] public bool canBeHurt = true;

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         StartCoroutine(CoFlash());
    //     }
    // }

    public IEnumerator CoFlash()
    {
        int count = 0;
        canBeHurt = false;

        while (count < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);

            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);

            count++;
        }
        canBeHurt = true;
    }

}
