using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Camera mainCam;

    private void Start(){
//        DamagePopup.Create(Vector3.zero, 400f);
    }

    void Awake(){
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate(){
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0)){
            Debug.Log(mousePos);
            DamagePopup.Create(mousePos, 400f);
        }
    }

}
