using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;

    public int playerNumber; 
    public string playerName;

    private Vector3 startPosition;
    private float movement;

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
            {
                movement -= 1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                movement += 1f;
            }
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement -= 1f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement += 1f;
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
        Vector3 movementVector = new Vector3(movement * speed * Time.deltaTime, 0f, 0f);
        transform.position += movementVector;

        // �̵� �� ������ �ʱ�ȭ
        movement = 0f;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
