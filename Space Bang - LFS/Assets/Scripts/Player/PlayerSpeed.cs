using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    #region Declarations
    [SerializeField] private float maxSpeed;
    public float currentSpeed { get; private set; }
    #endregion

    #region Speed
    void Awake(){
        currentSpeed = maxSpeed;
    }

    public void UpdateSpeed(float mod)
    {
        currentSpeed += mod;
    }
    #endregion

}
