using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage { get; private set; } = 20f;

    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float rotZ;
    public float force;

    private Vector3 direction, rotation;
    private Statistics playerStatistics;

    [SerializeField] private GameObject floatingTextPrefab;

    void Awake(){
        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<Statistics>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Start()
    {
        StartConfiguration();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    private void OnEnable(){
        if (rb != null){
            StartConfiguration();

            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        Invoke("Disable", 2f);
    }

    void StartConfiguration(){
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        rotation = transform.position - mousePos;

        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 90);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerStatistics.updateDamage(bulletDamage);
            DamagePopup.Create(other.transform.position, bulletDamage);

            EnemyHealth EnemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            EnemySpawnPosition EnemySpawnPosition = other.gameObject.GetComponent<EnemySpawnPosition>();
            EnemyHealth.UpdateHealth(-bulletDamage);

            if (EnemyHealth.currentHealth <= 0f){
                playerStatistics.updateKills();

                EnemyHealth.RestartHealth();
                EnemySpawnPosition.SpawnInRange(other.gameObject);
                other.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

    void ShowDamage(string damage)
    {
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
//        floatingTextPrefab.GetComponentInChildren<TextMesh>().text = damage;
    }

    void Disable(){
        gameObject.SetActive(false);
    }

    private void OnDisable(){
        CancelInvoke();
    }

}