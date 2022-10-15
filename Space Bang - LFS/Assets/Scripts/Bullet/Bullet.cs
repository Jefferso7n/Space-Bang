using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private float rotZ;
    public float force;

    private Vector3 direction, rotation;

    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] DamageDealer damageDealer;

    void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Start()
    {
        StartConfiguration();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            StartConfiguration();

            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        Invoke("Disable", 2f);
    }

    void StartConfiguration()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        rotation = transform.position - mousePos;

        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 90);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamagePopup.Create(other.transform.position, damageDealer.GetDamage());
        scoreKeeper.ModifyDamage(damageDealer.GetDamage());

        EnemyHealth EnemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        EnemySpawnPosition EnemySpawnPosition = other.gameObject.GetComponent<EnemySpawnPosition>();
        EnemyHealth.TakeDamage(damageDealer.GetDamage());

        if (EnemyHealth.GetCurrentHealth() <= 0)
        {
            EnemyHealth.RestartHealth();
            EnemySpawnPosition.SpawnInRange(other.gameObject);
            other.gameObject.SetActive(false);
        }
        gameObject.SetActive(false); //Disable bullet
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}