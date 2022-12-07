using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    #region Declarations
    [SerializeField] float speed = 2f;
    [SerializeField] float duration = 1.5f;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] PlayerController playerController;

    private float currentTime = 0f;
    #endregion

    #region Speed
    public float GetSpeed()
    {
        return speed * speedCurve.Evaluate(TimeManagement(playerController.isMoving));
    }

    public float GetMaxSpeed(){
        return speed;
    }
    #endregion

    #region Time
    private float TimeManagement(bool isMoving)
    {
        if (isMoving)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= duration)
            {
                currentTime = duration;
            }
        }
        else
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
            }

        }

        return currentTime / duration;
    }
    #endregion
}