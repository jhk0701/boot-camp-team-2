using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;
    public GameObject Paddle;
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
        if (collision.collider.gameObject.CompareTag("TopWall"))
        {
            
        }
        else if (collision.collider.gameObject.CompareTag("DownWall"))
        {

        }
        else if (collision.collider.gameObject.CompareTag("LeftWall"))
        {

        }
        else if (collision.collider.gameObject.CompareTag("DownWall"))
        {

        }
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody.velocity = new Vector2(x * speed, 1.0f * speed);
    }

    public void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        Vector3 ResetPosition = Paddle.transform.position;
        transform.position = new Vector2(ResetPosition.x, ResetPosition.y + 0.175f);
    }

    private void Bounce()
    {

    }
}
