using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage { get; private set; } = 20f;

    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    private float rotZ;

    private Vector3 direction, rotation;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.black; //Change enemy colour when hit
            other.gameObject.GetComponent<EnemyHealth>().UpdateHealth(-bulletDamage);

            if (other.gameObject.GetComponent<EnemyHealth>().currentHealth == 0f){ //Change the color and the health to the standard
                other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                other.gameObject.GetComponent<EnemyHealth>().currentHealth = other.gameObject.GetComponent<EnemyHealth>().maxHealth;
                other.gameObject.GetComponent<EnemySpawnPosition>().SpawnInRange(other.gameObject);
                other.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(false);
        }
    }

    void Disable(){
        gameObject.SetActive(false);
    }

    private void OnDisable(){
        CancelInvoke();
    }

}