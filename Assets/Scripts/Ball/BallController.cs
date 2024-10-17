using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;
    public GameObject Paddle;
    private Vector2 tempVelocity;
    private bool moving = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Paddle = GetComponent<GameObject>();
    }

    private void Update()
    {
        if ((moving is false) && (Input.GetKeyDown(KeyCode.Space)))
        {
            moving = true;
            Launch();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision);

        Brick brick = collision.gameObject.GetComponent<Brick>();
        brick?.Hit();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody.velocity = new Vector2(x * speed, 1.0f * speed);
        tempVelocity = rigidbody.velocity;
    }

    public void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        moving = false;
        Vector3 ResetPosition = Paddle.transform.position;
        transform.position = new Vector2(ResetPosition.x, ResetPosition.y + 0.175f);
    }

    private void Bounce(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("TopWall") || collision.collider.gameObject.CompareTag("DownWall") || collision.collider.gameObject.CompareTag("Brick"))
        {
            rigidbody.velocity = new Vector2(tempVelocity.x, -tempVelocity.y);
        }
        else if (collision.collider.gameObject.CompareTag("LeftWall") || collision.collider.gameObject.CompareTag("RightWall"))
        {
            rigidbody.velocity = new Vector2(-tempVelocity.x, tempVelocity.y);
        }
        else if (collision.collider.gameObject.CompareTag("Paddle"))
        {
            // 0.1f는 공의 반지름, 즉 공이 패들의 옆에서 충돌한다면
            if (transform.position.y - collision.transform.position.y < 0.1f)
            {
                rigidbody.velocity = new Vector2(-tempVelocity.x, tempVelocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(tempVelocity.x, -tempVelocity.y);
            }
        }
        tempVelocity = rigidbody.velocity;
    }
}
