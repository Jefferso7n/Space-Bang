using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;
        Vector2 mouseCursosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursosPos;
    }
}
