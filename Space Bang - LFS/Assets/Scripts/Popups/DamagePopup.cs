using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    //Create a damage popup
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    #region Declarations
    private const float DISAPPEAR_TIMER_MAX = .5f;
    private TextMeshPro textmesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    #endregion

    private void Awake()
    {
        textmesh = transform.GetComponent<TextMeshPro>();
    }

    private void FixedUpdate()
    {
        transform.position += moveVector * Time.fixedDeltaTime;
        moveVector -= moveVector * 10f * Time.fixedDeltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5)
        {
            //First half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.fixedDeltaTime;
        }
        else
        {
            //Second half of the popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.fixedDeltaTime;
        }

        disappearTimer -= Time.fixedDeltaTime;
        if (disappearTimer < 0)
        {
            //Start disappearing
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.fixedDeltaTime;
            textmesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(int damageAmount)
    {
        textmesh.SetText(damageAmount.ToString());
        textColor = textmesh.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        moveVector = new Vector3(.6f, 1) * 4f;
    }
}
