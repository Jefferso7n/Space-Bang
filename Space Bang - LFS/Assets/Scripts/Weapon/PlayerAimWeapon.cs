using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public Transform aimTransform;
    public Transform player;
    bool isfacingRight = true;

    void FixedUpdate()
    {
        HandleAiming();
    }

    [HideInInspector] public Camera mainCam;
    private Vector3 mousePosition;

    void HandleAiming()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;

        //        Debug.Log("Angle: " + angle);
        if (isfacingRight)
        {
            if (angle > 90 || angle < -90)
            {
                aimLocalScale.y = -1f;
            }
            else
            {
                aimLocalScale.y = 1f;
            }
        }
        else
        {
            if (angle > 90 || angle < -90)
            {
                aimLocalScale.y = 1f;
            }
            else
            {
                aimLocalScale.y = -1f;
            }
        }

        if (isfacingRight)
        {
            aimTransform.localScale = aimLocalScale;
        }
        else
        {
            aimTransform.localScale = aimLocalScale * -1;
        }

    }

    public void PositionController()
    {
        isfacingRight = !isfacingRight;
    }

}