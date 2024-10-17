using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int playerLives = 3;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //처음에 Start상태로 변환(알림)
        StateManager.Instance.SetState(StateManager.GameState.Start);

        // 벽돌 이벤트 구독
        BrickManager brickManager = FindObjectOfType<BrickManager>();
        if (brickManager != null)
        {
            brickManager.OnAllBrickBroken += HandleAllBricksBroken;
        }
    }


    private void HandleAllBricksBroken()
    {
        // 모든 벽돌이 부서지면 승리 상태로 전환
        StateManager.Instance.SetState(StateManager.GameState.Win);
        Debug.Log("Game Won!");
    }

    //TODO: BallController에서 이벤트 정의, 알림 후 구독
    private void HandleLifeLost()
    {
        Debug.Log("Life Lost!");
    }

    private void HandleAllLivesLost()
    {
        playerLives--;
        // 모든 목숨을 잃으면 패배 상태로 전환
        if (playerLives <= 0)
        {
            StateManager.Instance.SetState(StateManager.GameState.Lose);
            Debug.Log("Game Over - No Lives Left");
        }
    }
}

