using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // 적용 대상 : 스펙
    [SerializeField] private Stat baseStat;
    public Stat Stat 
    { 
        get { return baseStat; }
    }

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
        // 동일한 영역에서 겹쳐서 이동하도록 설정
        Vector3 movementVector = new Vector3(movement * Speed * Time.deltaTime, 0f, 0f);
        transform.position += movementVector;

        movement = 0f;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
