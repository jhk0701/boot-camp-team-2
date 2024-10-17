using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 


    public event Action OnLifeUpdate;

    private BrickManager brickManager;
    private BallMovement ballMovement;
    public LevelManager levelManager;
    private StateManager stateManager;

    public string player1Name;
    public string player2Name;

    private int lives = 3;


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

        levelManager = GetComponent<LevelManager>();
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
        brickManager = manager;
        brickManager.OnAllBrickBroken += HandleAllBricksBroken;
    }

    private void HandleAllBricksBroken()
    {
        stateManager.SetState(StateManager.GameState.Win);
    }

    private void HandleOnTouchBottom()
    {
        lives--;
        OnLifeUpdate?.Invoke();

        if (lives <= 0)
        {
            stateManager.SetState(StateManager.GameState.Lose);
        }
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


    public int GetCurrentLevel()
    {
        return levelManager.SelectedLevel;
    }

    public int GetCurrentStage()
    {
        return levelManager.SelectedStage;
    }
}
