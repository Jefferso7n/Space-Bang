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
    private float timeToLive = 4f;

    void Start()
    {
        Destroy(gameObject, timeToLive);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.black; //Change enemy colour when hit
            other.gameObject.GetComponent<EnemyHealth>().UpdateHealth(-bulletDamage);
            Destroy(gameObject);

            if (other.gameObject.GetComponent<EnemyHealth>().currentHealth == 0)
            {
                Destroy(other.gameObject);
            }
        }
    }

}