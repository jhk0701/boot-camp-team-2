using System;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // 적용 대상 : 스펙
    [SerializeField] private Stat baseStat;
    public Stat Stat 
    { 
        get { return baseStat; }
    }

    public Rigidbody2D RigidBody2d;
    // private bool moving = false;
    public bool IsMoving { get; set; } = false;

    public PaddleController PlayerPaddle;
    public int playerNumber;
    public string lastHitByPlayerName;

    // 임시로 패널 불러서 종료하기 위함
    public event Action OnTouchBottom;
    public static event Action<Vector3,int> OnPaddleHit;
    public static event Action<Vector3> OnWallHit;


    private void Awake()
    {
        GameManager.Instance.SetBallMovement(this);
    }

    void Start()
    {
        RigidBody2d = GetComponent<Rigidbody2D>();

        if(PlayerPaddle == null)
        {
            PaddleController[] paddles = FindObjectsOfType<PaddleController>();
            foreach (var paddle in paddles)
            {
                if (paddle.playerNumber == playerNumber)
                {
                    PlayerPaddle = paddle;
                    break;
                }
            }
        }

        lastHitByPlayerName = "";

        OnWallHit += (Vector3 vec) => 
        {
            GameManager.Instance.soundManager.PlaySfx(SfxType.WallHit);
        };

        OnPaddleHit += (Vector3 vec, int i) => 
        {
            GameManager.Instance.soundManager.PlaySfx(SfxType.PaddleHit);
        };
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
                brick.Hit(lastHitByPlayerName, Stat.damage);

                ScoreManager.Instance.AddScore(lastHitByPlayerName, 10);
                // Debug.Log($"Brick broken by {lastHitByPlayerName}, +10 points");
                
                if (brick.type.Equals(BrickType.Flow) || brick.type.Equals(BrickType.Penalty))
                {
                    ContactPoint2D contact = collision.GetContact(0);
                    
                    Vector2 dir = ((Vector2)transform.position - contact.point).normalized; // 들어온 방향으로 튕겨나오게
                    // Vector2 dir = (contact.point - (Vector2)transform.position).normalized; // 입사각
                    // Vector2 reflect = Vector2.Reflect(dir, contact.normal); // 반사각 계산

                    RigidBody2d.velocity = new Vector2(dir.x >= 0 ? Stat.speed : -Stat.speed, dir.y >= 0 ? Stat.speed : -Stat.speed);
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
        
        RigidBody2d.velocity = new Vector2(direction * Stat.speed, 1.0f * Stat.speed);
        IsMoving = true;
    }

    public void Reset()
    {
        RigidBody2d.velocity = Vector2.zero;
        // moving = false;
        IsMoving = false;
        // Vector3 ResetPosition = Paddle.transform.position;
        // transform.position = new Vector2(ResetPosition.x, ResetPosition.y + 0.175f);
    }

    private void TouchBottom()
    {
        OnTouchBottom?.Invoke();
        // 라이프 생기면 남은 라이프에 따라 리셋 정도만 시켜주기
        Reset();
    }

}