using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public enum GameState
    {
        Start,
        GameScene,
        Pause,
        Win,
        Lose
    }


    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;

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

    public void SetState(GameState newState)
    {
        //Debug.Log($"이전 상태 {CurrentState}");
        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
        //Debug.Log($"현재 상태 {CurrentState}");
    }
}
