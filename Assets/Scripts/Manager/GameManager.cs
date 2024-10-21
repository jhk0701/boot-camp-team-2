using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameMode
    {
        Single =0,
        Multi =1,
    }

    public GameMode gameMode = GameMode.Single;

    public static GameManager Instance; 

    public event Action<int> OnLifeUpdate;

    public LevelManager LevelManager { get; private set; }
    public BrickManager BrickManager { get; private set; }
    public ItemHandler ItemHandler { get; private set; }


    private BallMovement ballMovement;
    private StateManager stateManager;
    public SoundManager soundManager;

    public GameObject Paddle_Player2;
    public GameObject Ball_Player2;

    public string player1Name;
    public string player2Name;

    const int INITIALLIFE = 5;
    private int lives = INITIALLIFE;
    public int Lives 
    {
        get { return lives; }
        private set
        {
            lives = value;
            OnLifeUpdate?.Invoke(lives);

            if (lives <= 0)
            {
                stateManager.SetState(StateManager.GameState.Lose);
            }
        }
    }


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

        LevelManager = GetComponent<LevelManager>();
        stateManager = StateManager.Instance;
        soundManager = GetComponent<SoundManager>();
    }
   
    private void Start()
    {
        Application.targetFrameRate = 60;

        stateManager.SetState(StateManager.GameState.Start);
        SceneManager.sceneLoaded += OnSceneLoaded; 

    }


    public void SetSinglePlayMode()
    {
        gameMode = GameMode.Single;
        Debug.Log("Start SingPlayMode");
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
            if (gameMode == GameMode.Multi)
            {
                GameObject paddle2 = Instantiate(Paddle_Player2);
                GameObject ball2 = Instantiate(Ball_Player2);

                PaddleController paddleController2 = paddle2.GetComponent<PaddleController>();
                BallMovement ballMovement2 = ball2.GetComponent<BallMovement>();
                
                paddleController2.ballMovement = ballMovement2;
            }
        }
    }

    public void SetBallMovement(BallMovement ball)
    {
        ballMovement = ball;
        ballMovement.OnTouchBottom += HandleOnTouchBottom;
    }

    public void SetBrickManager(BrickManager manager)
    {
        BrickManager = manager;
        BrickManager.OnAllBrickBroken += HandleAllBricksBroken;
    }

    private void HandleAllBricksBroken()
    {
        stateManager.SetState(StateManager.GameState.Win);
    }

    private void HandleOnTouchBottom()
    {
        Lives--;
    }

    public void AddLife(int amount)
    {
        Lives += amount;
    }
    
    public void SetItemHandler(ItemHandler handler)
    {
        ItemHandler = handler;
    }

    public void StartGameScene()
    {
        SceneManager.LoadScene(1);
        stateManager.SetState(StateManager.GameState.GameScene);

        Lives = INITIALLIFE;
    }

    public void BackToLobby()
    {   
        SceneManager.LoadScene(0);
        stateManager.SetState(StateManager.GameState.Start);
    }

    public int GetLives()
    {
        return lives;
    }

}
