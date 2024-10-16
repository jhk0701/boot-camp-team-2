using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public float maxTime = 60f; 
    private float remainTime; 
    private float elapsedTime = 0f;
    private bool isPlaying; 

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

    private void OnEnable()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.GameScene:
                StartTimer();
                break;
            case GameManager.GameState.Pause:
                PauseTimer();
                break;
            case GameManager.GameState.Lobby:
            case GameManager.GameState.Win:
            case GameManager.GameState.Lose:
                StopTimer();
                break;
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            remainTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;

            if (remainTime <= 0)
            {
                GameManager.Instance.SetState(GameManager.GameState.Lose);
            }
        }
    }

    public void StartTimer()
    {
        remainTime = maxTime;
        elapsedTime = 0f;
        isPlaying = true;
    }

    public void StopTimer()
    {
        isPlaying = false;
    }

    public void PauseTimer()
    {
        isPlaying = false;
    }

    public void ResumeTimer()
    {
        isPlaying = true;
    }

    public float GetRemainingTime()
    {
        return remainTime;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
