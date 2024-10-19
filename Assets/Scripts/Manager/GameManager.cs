using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public event Action OnLifeUpdate;

    public LevelManager LevelManager { get; private set; }
    public BrickManager BrickManager { get; private set; }
    private BallMovement ballMovement;
    private StateManager stateManager;

    public string player1Name;
    public string player2Name;

    private int lives = 3;
    public int Lives 
    {
        get { return lives; }
        private set
        {
            lives = value;
            OnLifeUpdate?.Invoke();

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
    }
   
    private void Start()
    {
        stateManager.SetState(StateManager.GameState.Start);
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

}
