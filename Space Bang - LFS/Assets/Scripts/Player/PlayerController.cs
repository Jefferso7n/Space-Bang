using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Declarations
    public Rigidbody2D rb;
    Vector2 movement;
    [HideInInspector] public bool facingRight = true, isMoving = false;
    public PlayerAimWeapon playerAimWeapon;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerSpeed playerSpeed;
    [SerializeField] SFXPlayer sfxPlayer;
    #endregion

    void FixedUpdate()
    {
        if (playerHealth.IsAlive())
        {
            // Get inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement == Vector2.zero)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
                sfxPlayer.PlayFlyingClip();
            }

            // Move
            rb.MovePosition(rb.position + movement * playerSpeed.GetSpeed() * Time.fixedDeltaTime);

            // Flip player character after changing horizontal movement
            if (movement.x > 0f && !facingRight)
            {
                Flip();
            }
            else if (movement.x < 0f && facingRight)
            {
                Flip();
            }
        }
    }

    // Flips player character / adjusts weapon position
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;

        playerAimWeapon.PositionController();
    }
}