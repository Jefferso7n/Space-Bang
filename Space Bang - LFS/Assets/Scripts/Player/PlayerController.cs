using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector2 movement;
    public bool facingRight = true;
    public PlayerAimWeapon playerAimWeapon;

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * gameObject.GetComponent<PlayerSpeed>().currentSpeed * Time.fixedDeltaTime);

        if (movement.x > 0f && !facingRight){
            Flip();
        }else if(movement.x < 0f && facingRight){
            Flip();
        }

    }

    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;

        playerAimWeapon.PositionController();
    }
}