using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFlowing : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Vector2 point = collision.GetContact(0).point;
            Vector2 dir = ((Vector2)transform.position - point).normalized;

            rb.AddForce(dir * 1.5f, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
