using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Single = 0,
        Multi = 1,
    }

    public GameMode gameMode = GameMode.Single;

    public static GameManager Instance;

    public event Action<string, int> OnLifeUpdate; // �÷��̾� �̸��� ������ �� ����

    public LevelManager LevelManager { get; private set; }
    public BrickManager BrickManager { get; private set; }
    public ItemHandler ItemHandler { get; private set; }

    private StateManager stateManager;
    public SoundManager soundManager;

    public GameObject Paddle_Player1;
    public GameObject Ball_Player1;
    public GameObject Paddle_Player2;
    public GameObject Ball_Player2;

    public string player1Name;
    public string player2Name;

    const int INITIALLIFE = 3;
    private Dictionary<string, int> playerLives = new Dictionary<string, int>();
    public event Action OnBrickManagerSet;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LevelManager = GetComponent<LevelManager>();
            stateManager = StateManager.Instance;
            soundManager = GetComponent<SoundManager>();

            player1Name = "Player1";
            player2Name = "Player2";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        stateManager.SetState(StateManager.GameState.Start);
        SceneManager.sceneLoaded += OnSceneLoaded;



        playerLives[player1Name] = 3;
        playerLives[player2Name] = 3;
    }

    public void SetSinglePlayMode()
    {
        gameMode = GameMode.Single;
        Debug.Log("Start SinglePlayMode");
    }

    public void SetMultiPlayMode()
    {
        gameMode = GameMode.Multi;
        Debug.Log("Start MultiPlayMode");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            // �÷��̾� 1 ����
            GameObject paddle1 = Instantiate(Paddle_Player1);
            GameObject ball1 = Instantiate(Ball_Player1);

            PaddleController paddleController1 = paddle1.GetComponent<PaddleController>();
            BallMovement ballMovement1 = ball1.GetComponent<BallMovement>();

            paddleController1.ballMovement = ballMovement1;

            // �÷��̾� 1�� �̸��� ��ȣ ����
            paddleController1.playerName = player1Name;
            paddleController1.playerNumber = 1;

            ballMovement1.playerName = player1Name;
            ballMovement1.playerNumber = 1; // �߰��� �κ�

            SetBallMovement(ballMovement1);

            if (gameMode == GameMode.Multi)
            {
                // �÷��̾� 2 ����
                GameObject paddle2 = Instantiate(Paddle_Player2);
                GameObject ball2 = Instantiate(Ball_Player2);

                PaddleController paddleController2 = paddle2.GetComponent<PaddleController>();
                BallMovement ballMovement2 = ball2.GetComponent<BallMovement>();

                paddleController2.ballMovement = ballMovement2;

                // �÷��̾� 2�� �̸��� ��ȣ ����
                paddleController2.playerName = player2Name;
                paddleController2.playerNumber = 2;

                ballMovement2.playerName = player2Name;
                ballMovement2.playerNumber = 2; // �߰��� �κ�



                SetBallMovement(ballMovement2);
            }
        }
    }

    public int GetLives(string playerName)
    {
        if (playerLives.ContainsKey(playerName))
        {
            return playerLives[playerName];
        }
        return INITIALLIFE; // �⺻ ������ ��ȯ
    }

    private void SetLives(string playerName, int lives)
    {
        // �������� ������ ���� �ʵ��� 0���� ����
        lives = Mathf.Max(0, lives);

        playerLives[playerName] = lives;
        OnLifeUpdate?.Invoke(playerName, lives);

        if (lives <= 0)
        {
            Debug.Log($"{playerName}�� �������� ��� �����Ǿ����ϴ�.");

            // ��� �÷��̾��� �������� �����Ǿ����� Ȯ��
            bool allPlayersLost = true;

            if (gameMode == GameMode.Single)
            {
                // �̱� �÷��� ��忡���� �÷��̾� 1�� üũ
                if (playerLives[player1Name] > 0)
                {
                    allPlayersLost = false;
                }
            }
            else
            {
                // ��Ƽ �÷��� ��忡���� ��� �÷��̾� üũ
                foreach (var player in playerLives.Keys)
                {
                    if (playerLives[player] > 0)
                    {
                        allPlayersLost = false;
                        break;
                    }
                }
            }

            if (allPlayersLost)
            {
                stateManager.SetState(StateManager.GameState.Lose);
            }
        }
    }



    public void SetBallMovement(BallMovement ball)
    {
        ball.OnTouchBottom += HandleOnTouchBottom;
    }

    private void HandleAllBricksBroken()
    {
        stateManager.SetState(StateManager.GameState.Win);
    }

    private void HandleOnTouchBottom(int playerNumber)
    {
        string playerName = GetPlayerNameByNumber(playerNumber);
        if (playerName != null)
        {
            int currentLives = GetLives(playerName);
            if (currentLives > 0)
            {
                currentLives--;
                SetLives(playerName, currentLives);
            }
        }
    }

    private string GetPlayerNameByNumber(int playerNumber)
    {
        if (playerNumber == 1)
            return player1Name;
        else if (playerNumber == 2)
            return player2Name;
        else
        {
            Debug.LogError($"Invalid playerNumber: {playerNumber}");
            return null;
        }
    }

    public void SetBrickManager(BrickManager manager)
    {
        BrickManager = manager;
        BrickManager.OnAllBrickBroken += HandleAllBricksBroken;

        OnBrickManagerSet?.Invoke(); // BrickManager�� �����Ǿ����� �˸�
    }

    public void AddLife(string playerName, int amount)
    {
        int currentLives = GetLives(playerName);
        currentLives += amount;
        SetLives(playerName, currentLives);
    }

    public void SetItemHandler(ItemHandler handler)
    {
        ItemHandler = handler;
    }

    public void StartGameScene()
    {
        SceneManager.LoadScene(1);
        stateManager.SetState(StateManager.GameState.GameScene);

        playerLives.Clear();
        playerLives[player1Name] = INITIALLIFE;

        if (gameMode == GameMode.Multi)
        {
            playerLives[player2Name] = INITIALLIFE;
        }
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene(0);
        stateManager.SetState(StateManager.GameState.Start);
    }
}
