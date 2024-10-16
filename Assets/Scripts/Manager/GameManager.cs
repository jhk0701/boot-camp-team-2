using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    //State 관리
    private IGameState currentState;
    public LobbyState lobbyState { get; private set; }
    public GameSceneState gameSceneState { get; private set; }
    public PauseState pauseState { get; private set; }
    public WinState winState { get; private set; }
    public LoseState loseState { get; private set; }

    private int lifeCount;
    private int brickCount;
    public TimeManager timeManager;
    public LevelManager levelManager;

    private void Awake()
    {
        if(Instance == null)
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
        lobbyState = new LobbyState(this);
        gameSceneState = new GameSceneState(this);
        pauseState = new PauseState(this);
        loseState = new LoseState(this);
        winState = new WinState(this);

        SetState(lobbyState);
    }

    private void Update()
    {
        currentState?.UpdateState(); 
    }

    public int GetLifeCount()
    {
        return lifeCount;
    }

    public int GetBrickCount()
    {
        return brickCount;
    }


    public void SetState(IGameState newState)
    {
        //State가 바뀌면 이전 State에서는 나가기
        currentState?.ExitState(); 
        
        //현재 씬 변경
        currentState = newState;

        currentState.EnterState();
    }


    public void PauseGame()
    {
        SetState(pauseState);
    }

    public void GameOver()
    {
        SetState(loseState);
    }

    public void GameWin()
    {
        SetState(winState);
    }


}
