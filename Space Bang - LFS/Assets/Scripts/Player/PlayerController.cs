using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Declarations
    public Rigidbody2D rb;
    Vector2 movement;
    public bool facingRight = true;
    public PlayerAimWeapon playerAimWeapon;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerSpeed playerSpeed;
    #endregion

    void FixedUpdate()
    {
        if (playerHealth.IsAlive())
        {
            // Get inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Move
            rb.MovePosition(rb.position + movement * playerSpeed.currentSpeed * Time.fixedDeltaTime);

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