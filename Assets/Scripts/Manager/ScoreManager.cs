﻿using System;
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

    public string player1Name { get; set; }
    public string player2Name { get; set; }

    private GameManager gameManager;

    private string filePath;

    private int currentLevel;
    private int currentStage;

    private List<ScoreData> playerScores = new List<ScoreData>();

    public event Action<string, int> OnUpdateScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            filePath = Application.persistentDataPath + "/scores.json";
            Debug.Log("Persistent Data Path: " + Application.persistentDataPath);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;
        gameManager = GameManager.Instance;

        player1Name = "Player1";
        player2Name = "Player2";

        if (GameManager.Instance.BrickManager != null)
        {
            GameManager.Instance.BrickManager.OnBrickBroken += HandleOnBrickBroken;
        }
        else
        {
            // BrickManager가 아직 설정되지 않았다면, 설정되었을 때 구독하도록 이벤트 등록
            GameManager.Instance.OnBrickManagerSet += HandleOnBrickManagerSet;
        }

        LoadScores();
    }



    private void HandleOnStateChanged(StateManager.GameState gameState)
    {
        switch (gameState)
        {
            case StateManager.GameState.Start:
                break;
            case StateManager.GameState.GameScene:

                LoadOrCreatePlayerData(player1Name);
                if (gameManager.gameMode == GameManager.GameMode.Multi)
                {
                    LoadOrCreatePlayerData(player2Name);
                }

                LevelManager levelManager = GameManager.Instance.LevelManager;
                currentLevel = levelManager.SelectedLevel;
                currentStage = levelManager.SelectedStage;
                ResetCurrentScores();

                break;
            case StateManager.GameState.Win:
            case StateManager.GameState.Lose:

                CheckAndUpdateHighScore(player1Name);

                // 멀티 플레이 모드일 때만 플레이어 2의 스코어 처리
                if (gameManager.gameMode == GameManager.GameMode.Multi)
                {
                    CheckAndUpdateHighScore(player2Name);
                }

                SaveScores();
                break;
        }
    }

    private void HandleOnBrickManagerSet()
    {
        if (GameManager.Instance.BrickManager != null)
        {
            GameManager.Instance.BrickManager.OnBrickBroken += HandleOnBrickBroken;
        } 
    }

    private void HandleOnBrickBroken(Brick brick, string playerName)
    {
        AddScore(playerName, 10);
        Debug.Log($"Brick broken by {playerName}, +10 points");
    }

    public void AddScore(string playerName, int points)
    {
        if (string.IsNullOrEmpty(playerName))
            return;

        if (points <= 0)
            return;

        // 플레이어 데이터 찾기
        ScoreData playerData = playerScores.Find(p => p.playerName == playerName);

        // 플레이어 데이터가 없는 경우 (싱글 플레이 모드에서 player2의 점수를 처리하지 않기 위해)
        if (playerData == null)
            return;

        // 현재 레벨과 스테이지에 해당하는 StageScore 찾기 또는 생성
        StageScore stageScore = playerData.stageScores.Find
            (s => s.levelNumber == currentLevel && s.stageNumber == currentStage);
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
        Debug.Log(playerName);
        Debug.Log(stageScore.currentScore);

        // 스코어 업데이트 이벤트 호출
        OnUpdateScore?.Invoke(playerName, stageScore.currentScore);
    }

    public int GetCurrentScore(string playerName)
    {
        return GetCurrentScore(playerName, currentLevel, currentStage);
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

    public int GetHighScore(string playerName)
    {
        return GetHighScore(playerName, currentLevel, currentStage);
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

            if (playerScores == null)
            {
                Debug.LogWarning("Failed to deserialize scores.json, initializing empty scores");
                playerScores = new List<ScoreData>();
            }
            else
            {
                Debug.Log("Scores loaded from " + filePath);
            }
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
            // 멀티 플레이 모드가 아니고, 플레이어가 player2인 경우 무시
            if (gameManager.gameMode != GameManager.GameMode.Multi && playerData.playerName == player2Name)
                continue;

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
