using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{

    #region Declarations
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] SFXPlayer sfxPlayer;

    [Header("CameraShake")]
    [SerializeField] float cameraShakeIntensity = 1.5f;
    [SerializeField] float cameraShakeTimer = 0.15f;

    [Header("Bullet Attributes")]
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform aimWeaponOrigin;

    [SerializeField] private int numberOfBullets = 3;
    [SerializeField] private float bulletForce = 10f;
    [Range(0, 360)]
    [SerializeField] private float angleSpread = 20;
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

        if (Input.GetMouseButton(0) && canFire && playerHealth.IsAlive())
        {
            canFire = false; // Enter cooldown
            Shoot();
            sfxPlayer.PlayShootingClip();
            CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer);
        }
    }

    private void Shoot()
    {
        float angleStep = angleSpread / numberOfBullets;
        float aimingAngle = aimWeaponOrigin.rotation.eulerAngles.z;
        float centeringOffset = (angleSpread / 2) - (angleStep / 2); //offsets every projectile so the spread is centered on the mouse cursor

        for (int i = 0; i < numberOfBullets; i++)
        {
            float currentBulletAngle = angleStep * i;

            GameObject bullet = objectPooler.GetPooledObject(); // Bullet is the pooled object
            if (bullet == null) return;

            bullet.transform.position = bulletSpawnPosition.position;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulletAngle - centeringOffset));

            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }
}