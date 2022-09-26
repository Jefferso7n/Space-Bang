using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] AudioPlayer audioPlayer;

    void FixedUpdate()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire && playerHealth.IsAlive())
        {
            canFire = false;
            GameObject obj = ObjectPooler.current.GetPooledObject();
            if (obj == null) return;

            obj.transform.position = bulletTransform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
            audioPlayer.PlayShootingClip();
        }
    }
}
