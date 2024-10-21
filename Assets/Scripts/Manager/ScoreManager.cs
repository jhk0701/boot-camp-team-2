using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class StageScore
{
    public int levelNumber;
    public int stageNumber;
    public int currentScore;
    public int highScore;
}

[Serializable]
public class ScoreData
{
    public string playerName;
    public List<StageScore> stageScores = new List<StageScore>();
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private LevelManager levelManager;
    private GameManager gameManager;
    private BrickManager brickManager;
    private BallMovement ballMovement;

    public GameObject scoreBoardUIPrefab;
    private GameObject instantiatedScoreBoardUI;

    private string filePath;

    // 현재 레벨과 스테이지
    private int currentLevel;
    private int currentStage;

    public string player1Name;
    public string player2Name;

    //각 플레이어의 점수를 저장하는 리스트
    private List<ScoreData> playerScores = new List<ScoreData>();

    // 스코어 업데이트 이벤트 (플레이어 이름과 새로운 스코어 전달)
    public event Action<string, int> OnUpdateScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            filePath = Application.persistentDataPath + "/scores.json";

            LoadScores();

            levelManager = GetComponent<LevelManager>();
            gameManager = GetComponent<GameManager>();

            player1Name = "Player1";
            player2Name = "Player2";

            // if (gameManager != null)
            // {
            //     //player1Name = gameManager.player1Name;
            //     //player2Name = gameManager.player2Name;
            //     player1Name = "Player1";
            //     player2Name = "Player2";
            // }
            // else
            // {
            //     player1Name = "Player1";
            //     player2Name = "Player2";
            // }

            // 각 플레이어의 데이터를 로드하거나 생성
            LoadOrCreatePlayerData(player1Name);
            LoadOrCreatePlayerData(player2Name);
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

    private void HandleOnStateChanged(StateManager.GameState gameState)
    {
        switch (gameState)
        {
            case StateManager.GameState.Start:
                break;
            case StateManager.GameState.GameScene:

                LevelManager levelManager = GameManager.Instance.LevelManager;
                currentLevel = levelManager.SelectedLevel;
                currentStage = levelManager.SelectedStage;
                ResetCurrentScores();

                break;
            case StateManager.GameState.Pause:
                break;
            case StateManager.GameState.Win:
            case StateManager.GameState.Lose:

                // 각 플레이어의 최고 스코어 갱신
                CheckAndUpdateHighScore(player1Name);
                CheckAndUpdateHighScore(player2Name);
                
                SaveScores();
                break;
        }
    }

    public void SetBrickManager(BrickManager manager)
    {
        brickManager = manager;
        BrickManager.OnBrickBroken += HandleBrickBroken;
    }

    // HandleBrickBroken 메서드 수정: string playerName -> Brick brick
    private void HandleBrickBroken(Brick brick)
    {
        string playerName = brick.playerName;
        //TODO: 점수 차등으로 주기

        // 어떤 플레이어가 벽돌을 깼는지 식별
        AddScore(playerName, 10);

        Debug.Log($"AddScore +10 for {playerName}, Current Score: {GetCurrentScore(playerName)}");
    }

    public void AddScore(string playerName, int points)
    {
        if (points <= 0)
            return;

        // 플레이어 데이터 찾기
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);

        // 현재 레벨과 스테이지에 해당하는 StageScore 찾기 또는 생성
        StageScore stageScore = playerData.stageScores.Find(s => s.levelNumber == currentLevel && s.stageNumber == currentStage);
        if (stageScore == null)
        {
            stageScore = new StageScore
            {
                levelNumber = currentLevel,
                stageNumber = currentStage,
                currentScore = 0,
                highScore = 0
            };
            playerData.stageScores.Add(stageScore);
        }

        // 현재 스코어 업데이트
        stageScore.currentScore += points;

        // 스코어 업데이트 이벤트 호출
        OnUpdateScore?.Invoke(playerName, stageScore.currentScore);
    }


    public int GetCurrentScore(string playerName)
    {
        return GetCurrentScore(playerName, currentLevel, currentStage);
    }

    public int GetHighScore(string playerName)
    {
        return GetHighScore(playerName, currentLevel, currentStage);
    }

    public int GetCurrentScore(string playerName, int level, int stage)
    {
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);
        if (playerData == null)
            return 0;

        StageScore stageScore = playerData.stageScores.Find(s => s.levelNumber == level && s.stageNumber == stage);
        if (stageScore != null)
        {
            return stageScore.currentScore;
        }

        return 0;
    }

    public int GetHighScore(string playerName, int level, int stage)
    {
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);
        if (playerData == null)
            return 0;

        StageScore stageScore = playerData.stageScores.Find(s => s.levelNumber == level && s.stageNumber == stage);
        if (stageScore != null)
        {
            return stageScore.highScore;
        }

        return 0;
    }


    private void CheckAndUpdateHighScore(string playerName)
    {
        // 플레이어 데이터 찾기
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);
        if (playerData == null)
            return;

        // 현재 레벨과 스테이지에 해당하는 StageScore 찾기
        StageScore stageScore = playerData.stageScores.Find(s => s.levelNumber == currentLevel && s.stageNumber == currentStage);
        if (stageScore != null)
        {
            // 현재 점수가 기존의 최고 점수보다 높으면 업데이트
            if (stageScore.currentScore > stageScore.highScore)
            {
                stageScore.highScore = stageScore.currentScore;
                Debug.Log($"New high score for {playerName} at Level {currentLevel + 1}, Stage {currentStage + 1}: {stageScore.highScore}");
            }
        }
    }


    private void LoadOrCreatePlayerData(string playerName)
    {
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);
        if (playerData == null)
        {
            // 새로운 플레이어 데이터 생성
            playerData = new ScoreData
            {
                playerName = playerName
            };
            playerScores.Add(playerData);
            Debug.Log($"Created new ScoreData for player {playerName}");
        }
        else
        {
            Debug.Log($"Loaded existing ScoreData for player {playerName}");
        }
    }

    public void SaveScores()
    {
        string json = JsonConvert.SerializeObject(playerScores, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Debug.Log("Scores saved to " + filePath);
    }

    public void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerScores = JsonConvert.DeserializeObject<List<ScoreData>>(json);
            Debug.Log("Scores loaded from " + filePath);
        }
        else
        {
            Debug.Log("No existing score file found, starting with empty scores");
            playerScores = new List<ScoreData>();
        }
    }

    // 현재 스코어를 초기화 (새 게임 시작 시)
    public void ResetCurrentScores()
    {
        foreach (var playerData in playerScores)
        {
            StageScore stageScore = playerData.stageScores.Find(s => s.levelNumber == currentLevel && s.stageNumber == currentStage);
            if (stageScore != null)
            {
                stageScore.currentScore = 0;
                Debug.Log($"Reset score for Player: {playerData.playerName}, Level: {currentLevel}, Stage: {currentStage}");
            }
            else
            {
                // 해당 스테이지의 StageScore가 없으면 생성
                stageScore = new StageScore
                {
                    levelNumber = currentLevel,
                    stageNumber = currentStage,
                    currentScore = 0,
                    highScore = 0
                };
                playerData.stageScores.Add(stageScore);
                Debug.Log($"Created new StageScore for Player: {playerData.playerName}, Level: {currentLevel}, Stage: {currentStage}");
            }
        }
    }
}
