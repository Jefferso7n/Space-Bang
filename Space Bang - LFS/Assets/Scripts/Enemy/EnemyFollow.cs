using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed { get; private set; } = 2.5f;
    public float respeed { get; private set; }
    public float attackDamage { get; private set; } = 1f;
    public float attackSpeed { get; private set; } = 1.5f;
    public float canAttack;


    private Transform target;
    public Rigidbody2D rb;
    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        respeed = speed;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = target.position - transform.position;
        distance = distance.normalized;
        distance = distance * speed;

        rb.AddForce(distance);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
            canAttack = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.fixedDeltaTime;
            }
        }
    }

//     void OnCollisionExit2D(Collision2D other){
//         speed = 0f;
//         Debug.Log("Exit");
//     }

    // void OnTriggerStay2D(Collider2D other){
    //     if (other.gameObject.tag == "Player")
    //     {
    //         if (attackSpeed <= canAttack)
    //         {
    //             other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
    //             canAttack = 0f;
    //         }
    //         else
    //         {
    //             canAttack += Time.fixedDeltaTime;
    //         }
    //     }
    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
    //         canAttack = 0f;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         target = null;
    //     }
    // }
}
