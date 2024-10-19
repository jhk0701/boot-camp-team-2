using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // ���� ��� : ����
    // [SerializeField] private float speed = 5f;
    public float Speed { get; set; } = 5f;
    float size = 1f;
    public float Size 
    {
        get { return size; }
        set 
        {
            size = value;
            Vector3 scale = transform.localScale;
            scale.x = size;
            transform.localScale = scale;
        }
    }


    public int playerNumber; 
    public string playerName;

    private Vector3 startPosition;
    private float movement;

    public BallMovement ballMovement;
    // public event Action OnItemUsed;


    void Start()
    {
        startPosition = transform.position;

        // �÷��̾� ��ȣ�� ���� �÷��̾� �̸� ����
        if (playerNumber == 1)
        {
            playerName = ScoreManager.Instance.player1Name;
        }
        else if (playerNumber == 2)
        {
            playerName = ScoreManager.Instance.player2Name;
        }
    }

    void Update()
    {
        // �÷��̾� ��ȣ�� ���� �ٸ� �Է� Ű�� ����Ͽ� �е� �̵�
        
        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.A))
                movement = -1f;
            else if (Input.GetKey(KeyCode.D))
                movement = 1f;
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                movement = -1f;
            else if (Input.GetKey(KeyCode.RightArrow))
                movement = 1f;
        }

        if (!ballMovement.IsMoving)
        {
            ballMovement.transform.position = (Vector2)transform.position + Vector2.up * 0.175f;

            if ((playerNumber == 1 && Input.GetKeyDown(KeyCode.Space)) ||
                (playerNumber == 2 && Input.GetKeyDown(KeyCode.Return)))
            {
                ballMovement.Launch(movement);
            }
        }

        if (movement != 0f)
        {
            MovePaddle();
        }
    }

    void MovePaddle()
    {
        // ������ �������� ���ļ� �̵��ϵ��� ����
        Vector3 movementVector = new Vector3(movement * Speed * Time.deltaTime, 0f, 0f);
        transform.position += movementVector;

        movement = 0f;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
