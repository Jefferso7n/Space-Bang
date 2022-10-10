using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    //Confine cursor to the game window
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(gameObject);
    }
}
