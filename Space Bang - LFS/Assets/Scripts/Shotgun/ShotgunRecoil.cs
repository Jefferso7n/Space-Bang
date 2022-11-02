using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunRecoil : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float recoilStrength = 100f;
    [SerializeField] Transform aimWeaponOrigin;
    Vector2 direction;

    public void Recoil()
    {
        direction = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized; //aimWeaponOrigin.rotation.eulerAngles.z
        Debug.Log(direction);
        direction = direction * recoilStrength;

        playerRb.AddForce(direction, ForceMode2D.Impulse);
    }
}