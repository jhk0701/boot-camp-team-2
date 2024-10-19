using System;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // ���� ��� : ����
    [SerializeField] private float speed = 5f;
    public int Damage { get; set; } = 1;

    public Rigidbody2D rigidbody;
    public GameObject Paddle;
    // private bool moving = false;
    public bool IsMoving { get; set; } = false;

    public int playerNumber;
    public string lastHitByPlayerName;

    // �ӽ÷� �г� �ҷ��� �����ϱ� ����
    public event Action OnTouchBottom;
    public static event Action<Vector3,int> OnPaddleHit;
    public static event Action<Vector3> OnWallHit;

    private ItemBehaviour currentItemBehaviour;


    private void Awake()
    {
        GameManager.Instance.SetBallMovement(this);
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        // ��...����� �ʹ� ū ����Դϴ�.
        if(Paddle == null)
        {
            PaddleController[] paddles = FindObjectsOfType<PaddleController>();
            foreach (var paddle in paddles)
            {
                if (paddle.playerNumber == playerNumber)
                {
                    Paddle = paddle.gameObject;
                    break;
                }
            }
        }

        lastHitByPlayerName = "";
    }

    // private void Update()
    // {
    //     if (moving is false)
    //     {
    //         //Local MultiPlay Code
    //         if ((playerNumber == 1 && Input.GetKeyDown(KeyCode.Space)) ||
    //             (playerNumber == 2 && Input.GetKeyDown(KeyCode.Return)))
    //         {
    //             moving = true;
    //             Launch();
    //         }
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DownWall"))
        {
            TouchBottom();
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            lastHitByPlayerName = paddle.playerName;
            OnPaddleHit?.Invoke(transform.position, paddle.playerNumber);
            Debug.Log($"Ball was hit by {lastHitByPlayerName}");
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            if (brick != null)
            {
                brick.Hit(lastHitByPlayerName, Damage);

                ScoreManager.Instance.AddScore(lastHitByPlayerName, 10);
                // Debug.Log($"Brick broken by {lastHitByPlayerName}, +10 points");
                
                if(brick.type.Equals(BrickType.Flow))
                {
                    ContactPoint2D contact = collision.GetContact(0);
                    
                    Vector2 dir = (contact.point - (Vector2)transform.position).normalized;
                    Vector2 reflect = Vector2.Reflect(dir, contact.normal);

                    rigidbody.velocity = new Vector2(reflect.x >= 0 ? speed : -speed, reflect.y >= 0 ? speed : -speed);
                }

            }
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            OnWallHit?.Invoke(transform.position);
        }
    }


    public void Launch(float direction)
    {
        if(direction == 0f)
            direction = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        
        rigidbody.velocity = new Vector2(direction * speed, 1.0f * speed);
        IsMoving = true;
    }

    public void Reset()
    {
        rigidbody.velocity = Vector2.zero;
        // moving = false;
        IsMoving = false;
        // Vector3 ResetPosition = Paddle.transform.position;
        // transform.position = new Vector2(ResetPosition.x, ResetPosition.y + 0.175f);
    }

    private void TouchBottom()
    {
        OnTouchBottom?.Invoke();
        // ������ ����� ���� �������� ���� ���� ������ �����ֱ�
        Reset();
    }

    // TODO : �����丵�ϱ�

}

