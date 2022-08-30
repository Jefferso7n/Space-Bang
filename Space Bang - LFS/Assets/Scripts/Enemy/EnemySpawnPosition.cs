using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 11f;
    private Vector2 spawnPosition;
    private GameObject player;

    void Awake()
    {
//        Debug.Log("Awake");
        player = GameObject.Find("Player");
    }

    public Vector2 SpawnInRange(GameObject obj){
        spawnPosition = player.transform.position;
        spawnPosition += Random.insideUnitCircle.normalized * spawnRadius * 1.5f;
        if (Mathf.Abs(spawnPosition.x) > spawnRadius || Mathf.Abs(spawnPosition.y) > spawnRadius){
//            Debug.Log(spawnPosition);
            return spawnPosition;
        }
        return SpawnInRange(obj);
    }
}
