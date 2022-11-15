using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Responsible for managing enemy and player damage
    [SerializeField] int damage = 10;
    [SerializeField] int damageVariance = 3;

    public int GetDamage(){
//        return damage;
        return Random.Range(damage - damageVariance, damage + damageVariance);
    }

    // public void Hit(){
    //     Destroy(gameObject);
    // }
}
