using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public float totalDamage, enemiesKilled;

    public void updateDamage (float damage){
        totalDamage += damage;
        PlayerPrefs.SetFloat("totalDamage", totalDamage);
    }

    public void updateKills (){
        PlayerPrefs.SetFloat("enemiesKilled", ++enemiesKilled);
    }
}
