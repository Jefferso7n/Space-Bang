using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    public Texture2D crosshair;

    private void Awake(){
        instance = this;
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateCrosshairCursor(){
        Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
    }

    public void ClearCursor(){
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
