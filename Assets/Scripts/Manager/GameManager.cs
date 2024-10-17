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
        //ó���� Start���·� ��ȯ(�˸�)
        StateManager.Instance.SetState(StateManager.GameState.Start);

        // ���� �̺�Ʈ ����
        BrickManager brickManager = FindObjectOfType<BrickManager>();
        if (brickManager != null)
        {
            brickManager.OnAllBrickBroken += HandleAllBricksBroken;
        }
    }


    private void HandleAllBricksBroken()
    {
        // ��� ������ �μ����� �¸� ���·� ��ȯ
        StateManager.Instance.SetState(StateManager.GameState.Win);
        Debug.Log("Game Won!");
    }

    //TODO: BallController���� �̺�Ʈ ����, �˸� �� ����
    private void HandleLifeLost()
    {
        Debug.Log("Life Lost!");
    }

    private void HandleAllLivesLost()
    {
        playerLives--;
        // ��� ����� ������ �й� ���·� ��ȯ
        if (playerLives <= 0)
        {
            StateManager.Instance.SetState(StateManager.GameState.Lose);
            Debug.Log("Game Over - No Lives Left");
        }
    }
}

