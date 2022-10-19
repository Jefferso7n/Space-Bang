using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region Declarations
    private Camera mainCam;
    private Vector3 mousePos;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] SFXPlayer sfxPlayer;
    #endregion

    void FixedUpdate()
    {
        #region Cooldown Controller
        // If the shot IS on COOLDOWN, the timer will increase until shooting is possible. It will then set 'canFire' to true.
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true; // Exit cooldown
                timer = 0;
            }
        }
        #endregion

        #region Shooting
        // If the shot is NOT on COOLDOWN, and the player shoots then it will generate (enable) a bullet.
        // In addition to putting the shot on cooldown.
        if (Input.GetMouseButton(0) && canFire && playerHealth.IsAlive())
        {
            canFire = false; // Enter cooldown
            GameObject obj = objectPooler.GetPooledObject(); // Bullet is the pooled object
            if (obj == null) return;

            // Sets the bullet position on the weapon, activates it and plays the shooting audio
            obj.transform.position = bulletTransform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
            sfxPlayer.PlayShootingClip();
        }
        #endregion
    }
}
