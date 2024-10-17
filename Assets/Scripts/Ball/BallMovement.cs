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

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Paddle = GameObject.Find("Paddle");



        // �ӽ÷� �г� �ҷ��� �����ϱ� ����
        OnTouchBottom += GameManager.Instance.GameOver;
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
        // �ӽ÷� �г� �ҷ��� �����ϱ� ����
        OnTouchBottom?.Invoke();

        // TODO : ������ �پ�鵵�� �Ѵ�. ���ӸŴ����� ������ �ٿ��� �־�� �ҵ���
        // �׸��� ���� ������ ���� null�̶� ���� �߻�
        //if (GameManager.Instance.GetLifeCount() == 0)
        //{
        //      OnTouchBottom?.Invoke();
        //}
        Reset();
    }
}
