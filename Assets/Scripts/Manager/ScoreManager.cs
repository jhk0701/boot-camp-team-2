using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

[Serializable]
public class StageScore
{
    public int stageNumber;
    public int currentScore;
    public int highScore;
}

[Serializable]
public class LevelScore
{
    public int levelNumber;
    public List<StageScore> stageScores = new List<StageScore>();
}

[Serializable]
public class ScoreData
{
    public string playerName;
    public List<LevelScore> levelScores = new List<LevelScore>();
}


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private LevelManager levelManager;
    private GameManager gameManager;
    private BrickManager brickManager;
    private BallMovement ballMovement;

    private List<ScoreData> playerScores = new List<ScoreData>();
    public GameObject scoreBoardUIPrefab; // ScoreBoardUI 프리팹 참조
    private GameObject instantiatedScoreBoardUI;

    private string filePath;

    // 현재 레벨과 스테이지
    private int currentLevel;
    private int currentStage;

    // 플레이어 이름
    public string player1Name;
    public string player2Name;

    // 스코어 업데이트 이벤트 (플레이어 이름과 새로운 스코어 전달)
    public event Action<string, int> OnScoreUpdate;

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
            if (gameManager != null)
            {
                //player1Name = gameManager.player1Name;
                //player2Name = gameManager.player2Name;
                player1Name = "Player1";
                player2Name = "Player2";
            }
            else
            {
                player1Name = "Player1";
                player2Name = "Player2";
            }

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
                ResetCurrentScores();
                break;
            case StateManager.GameState.GameScene:
                LevelManager levelManager = GameManager.Instance.levelManager;
                currentLevel = levelManager.SelectedLevel;
                currentStage = levelManager.SelectedStage;
                break;
            case StateManager.GameState.Pause:
                break;
            case StateManager.GameState.Win:
            case StateManager.GameState.Lose:
                // 각 플레이어의 최고 스코어 업데이트
                CheckAndUpdateHighScore(player1Name);
                CheckAndUpdateHighScore(player2Name);
                if (scoreBoardUIPrefab != null)
                {
                    instantiatedScoreBoardUI = Instantiate(scoreBoardUIPrefab);
                    // 필요하다면 인스턴스 위치 및 부모 설정
                    // instantiatedScoreBoardUI.transform.SetParent(someParentTransform, false);
                }
                // 스코어 저장
                SaveScores();
                break;
        }
    }

    public void SetBrickManager(BrickManager manager)
    {
        brickManager = manager;
        brickManager.OnBrickBroken += HandleBrickBroken;
    }

    private void HandleBrickBroken(string playerName)
    {
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
        if (playerData == null)
        {
            Debug.LogError($"Player {playerName} not found in playerScores");
            return;
        }

        // 레벨 스코어 가져오기 또는 생성
        LevelScore levelScore = playerData.levelScores.Find(l => l.levelNumber == currentLevel);
        if (levelScore == null)
        {
            levelScore = new LevelScore { levelNumber = currentLevel };
            playerData.levelScores.Add(levelScore);
        }

        // 스테이지 스코어 가져오기 또는 생성
        StageScore stageScore = levelScore.stageScores.Find(s => s.stageNumber == currentStage);
        if (stageScore == null)
        {
            stageScore = new StageScore { stageNumber = currentStage };
            levelScore.stageScores.Add(stageScore);
        }

        // 현재 스코어 업데이트
        stageScore.currentScore += points;

        // 스코어 업데이트 이벤트 호출
        OnScoreUpdate?.Invoke(playerName, stageScore.currentScore);
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

        LevelScore levelScore = playerData.levelScores.Find(l => l.levelNumber == level);
        if (levelScore != null)
        {
            StageScore stageScore = levelScore.stageScores.Find(s => s.stageNumber == stage);
            if (stageScore != null)
            {
                return stageScore.currentScore;
            }
        }

        return 0;
    }

    public int GetHighScore(string playerName, int level, int stage)
    {
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);
        if (playerData == null)
            return 0;

        LevelScore levelScore = playerData.levelScores.Find(l => l.levelNumber == level);
        if (levelScore != null)
        {
            StageScore stageScore = levelScore.stageScores.Find(s => s.stageNumber == stage);
            if (stageScore != null)
            {
                return stageScore.highScore;
            }
        }

        return 0;
    }

    private void CheckAndUpdateHighScore(string playerName)
    {
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);
        if (playerData == null)
            return;

        LevelScore levelScore = playerData.levelScores.Find(l => l.levelNumber == currentLevel);
        if (levelScore != null)
        {
            StageScore stageScore = levelScore.stageScores.Find(s => s.stageNumber == currentStage);
            if (stageScore != null)
            {
                if (stageScore.currentScore > stageScore.highScore)
                {
                    stageScore.highScore = stageScore.currentScore;
                    Debug.Log($"New high score for {playerName} at Level {currentLevel}, Stage {currentStage}: {stageScore.highScore}");
                }
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
            LevelScore levelScore = playerData.levelScores.Find(l => l.levelNumber == currentLevel);
            if (levelScore != null)
            {
                StageScore stageScore = levelScore.stageScores.Find(s => s.stageNumber == currentStage);
                if (stageScore != null)
                {
                    stageScore.currentScore = 0;
                }
            }
        }
    }

}
