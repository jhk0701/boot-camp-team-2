using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidbody;
    public GameObject Paddle;
    private bool moving = false;

    // �ӽ÷� �г� �ҷ��� �����ϱ� ����
    public event Action OnTouchBottom;
    private void Awake()
    {
        GameManager.Instance.SetBallMovement(this);
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Paddle = GameObject.Find("Paddle");
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
        if(collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            GameManager.Instance.soundManager.PlaySfx(GameManager.Instance.soundManager.wallClip);
        }
        else if(collision.gameObject.CompareTag("Brick"))
        {
            GameManager.Instance.soundManager.PlaySfx(GameManager.Instance.soundManager.brickClip);
        }

        if (collision.collider.gameObject.CompareTag("DownWall"))
        {
            TouchBottom();
        }

        Brick brick = collision.gameObject.GetComponent<Brick>();
        brick?.Hit();
    }

    private void Launch()
    {
        float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;

        rigidbody.velocity = new Vector2(x * speed, 1.0f * speed);
    }

    public void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        moving = false;
        Vector3 ResetPosition = Paddle.transform.position;
        transform.position = new Vector2(ResetPosition.x, ResetPosition.y + 0.175f);
    }

    private void TouchBottom()
    {
        OnTouchBottom?.Invoke();
        // ������ ����� ���� �������� ���� ���� ������ �����ֱ�
        Reset();
    }
}
