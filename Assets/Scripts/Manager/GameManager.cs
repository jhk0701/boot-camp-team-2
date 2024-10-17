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
        Debug.Log($"lives Lost : -1!, Current Life: {lives}");

        if (lives <= 0)
        {
            stateManager.SetState(StateManager.GameState.Lose);
            Debug.Log("목숨 0개, 패배 전환");
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

}
