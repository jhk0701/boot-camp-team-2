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

        // 플레이어 번호에 따라 플레이어 이름 설정
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
        // 플레이어 번호에 따라 다른 입력 키를 사용하여 패들 이동
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
        // 동일한 영역에서 겹쳐서 이동하도록 설정
        Vector3 movementVector = new Vector3(movement * speed * Time.deltaTime, 0f, 0f);
        transform.position += movementVector;

        // 이동 후 움직임 초기화
        movement = 0f;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
