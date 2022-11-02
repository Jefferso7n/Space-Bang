using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    #region Declarations
    [HideInInspector] public Vector3 mousePosition;
    public Transform aimTransform;
    public Transform player;
    bool isfacingRight = true;
    #endregion

    #region Aiming
    private void Update() {
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = dir;
    }

    void FixedUpdate()
    {
        HandleAiming();
    }

    void HandleAiming()
    {
        // Get the mouse position, which is where the gun should be aiming
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;

        // Debug.Log("Angle: " + angle);
        // Controls the scale/direction of the weapon
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
    #endregion

}