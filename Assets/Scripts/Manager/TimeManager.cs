using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private float elapsedTime = 0f;
    private bool isPlaying;

    public event Action<float> OnUpdateTime;

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
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;
    }

    private void HandleOnStateChanged(StateManager.GameState newState)
    {
        switch (newState)
        {
            case StateManager.GameState.GameScene:
                ResumeTimer();
                break;
            case StateManager.GameState.Pause:
                PauseTimer();
                break;
            case StateManager.GameState.Start:
            case StateManager.GameState.Win:
            case StateManager.GameState.Lose:
                StopTimer();
                break;
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            elapsedTime += Time.deltaTime;
            OnUpdateTime?.Invoke(elapsedTime);
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isPlaying = true;
        Time.timeScale = 1f;
    }

    public void StopTimer()
    {
        isPlaying = false;
        Time.timeScale = 0f;
    }

    public void PauseTimer()
    {
        isPlaying = false;
        Time.timeScale = 0f;
    }

    public void ResumeTimer()
    {
        Time.timeScale = 1f;
        isPlaying = true;
    }
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
