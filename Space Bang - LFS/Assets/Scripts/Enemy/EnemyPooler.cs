using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public static EnemyPooler current;
    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow;

    private List<GameObject> pooledObjects;

    [SerializeField]
    private float time = 1f;
    // private Vector2 playerPosition;

    // void Awake()
    // {
    //     playerPosition = GameObject.Find("Player").transform.position;
    // }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        current = this;
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        StartCoroutine(SpawnAnEnemy());
    }

    public GameObject GetPooledObject(){
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }
        if (willGrow){
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

    void SpawnEnemy(){
//
//        SpawnInRange();
        GameObject obj = GetPooledObject();
        if (obj == null) return;

        obj.transform.position = obj.gameObject.GetComponent<EnemySpawnPosition>().SpawnInRange(obj);
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);
    }

    // public void SpawnInRange(){
    //     spawnPosition += Random.insideUnitCircle.normalized * spawnRadius;
    //     if (Vector2.Distance (playerPosition, spawnPosition) < spawnRadius-2f){
    //         SpawnInRange();
    //         Debug.Log("<");
    //     }else{
    //         return;
    //     }
    // }

    IEnumerator SpawnAnEnemy()
    {
        SpawnEnemy();            
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnAnEnemy());
    }

}
