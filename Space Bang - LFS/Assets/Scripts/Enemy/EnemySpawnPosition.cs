using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPosition : MonoBehaviour
{
    #region Declarations
    [SerializeField]
    private float spawnRadius = 11f;
    private Vector2 spawnPosition;
    private GameObject player;
    #endregion

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Generates initial spawn positions for enemies
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
