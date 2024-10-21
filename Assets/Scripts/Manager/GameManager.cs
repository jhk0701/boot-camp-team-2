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

    public string player1Name;
    public string player2Name;

    const int INITIAL_LIFE = 5;
    private int lives = INITIAL_LIFE;
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

        stateManager.OnStateChanged += HandleStateChanged;
        stateManager.SetState(StateManager.GameState.Start);
    }

    public void HandleStateChanged(StateManager.GameState gameState )
    {
        switch( gameState )
        {
            case StateManager.GameState.Start:
                break;

            case StateManager.GameState.GameScene:
                lives = 5;
                break;

            case StateManager.GameState.Pause:
                break;

            case StateManager.GameState.Win:
                break;

            case StateManager.GameState.Lose:
                break;  

        }
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

    public void InitLife()
    {
        Lives = INITIAL_LIFE;
    }

    public void SetItemHandler(ItemHandler handler)
    {
        ItemHandler = handler;
    }

    public void StartGameScene()
    {
        stateManager.SetState(StateManager.GameState.GameScene);
        SceneManager.LoadScene(1);
    }

    public void BackToLobby()
    {   
        stateManager.SetState(StateManager.GameState.Start);
        SceneManager.LoadScene(0);
    }

    public int GetLives()
    {
        return lives;
    }

}
