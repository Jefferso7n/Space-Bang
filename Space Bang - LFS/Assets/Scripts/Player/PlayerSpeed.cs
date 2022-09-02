using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    public float currentSpeed { get; private set; }

    void Awake(){
        currentSpeed = maxSpeed;
    }

    public void UpdateSpeed(float mod)
    {
        currentSpeed += mod;
    }
}
