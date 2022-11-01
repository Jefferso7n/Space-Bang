using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLook : MonoBehaviour
{
    void Update()
    {
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = dir;
    }
}