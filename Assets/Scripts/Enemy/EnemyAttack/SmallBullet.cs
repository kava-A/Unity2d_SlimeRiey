using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBullet : MonoBehaviour
{
    public float speed = 50f;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Destroy(gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(5);
            Destroy(gameObject);
        }

    }

}
