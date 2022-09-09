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

    void Awake()
    {
//        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        // mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // Vector3 rotation = mousePos - transform.position;
        // float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            GameObject obj = ObjectPooler.current.GetPooledObject();
            if (obj == null) return;

            obj.transform.position = bulletTransform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
        }
    }
}
