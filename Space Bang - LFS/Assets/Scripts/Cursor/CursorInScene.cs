using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInScene : MonoBehaviour
{
    void Start(){
        Cursor.visible = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
