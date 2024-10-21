using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private float elapsedTime = 0f;
    public bool isPlaying;

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

    void OnDisable()
    {
        StateManager.Instance.OnStateChanged -= HandleOnStateChanged;
    }

    private void HandleOnStateChanged(StateManager.GameState newState)
    {
        switch (newState)
        {
            case StateManager.GameState.GameScene:
                StartTimer();
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

    public void StopTimer()
    {
        elapsedTime = 0f;
        isPlaying = false;
        Time.timeScale = 0f;
    }
    public void PauseTime()
    {
        isPlaying = false;
        Time.timeScale = 0f;
    }
    public void StartTimer()
    {
        elapsedTime = 0f;
        Time.timeScale = 1f;
        isPlaying = true;
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
