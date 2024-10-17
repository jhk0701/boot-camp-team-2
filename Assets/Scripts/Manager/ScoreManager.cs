using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public struct ScoreData
{
    public int score;
    public string playerName;
    public string date;
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private BrickManager brickManager;
    private GameManager gameManager;
    private List<ScoreData> scoreList = new List<ScoreData>();

    private string filePath;

    // 플레이어 이름과 점수를 저장할 딕셔너리
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();

    // 게임 매니저에서 가져올 플레이어 이름
    private string player1Name;
    private string player2Name;

    public event Action<string, int> OnScoreUpdate; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Application.persistentDataPath + "/scores.json"; // 점수 데이터 파일 경로 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager != null)
        {
            //TODO:
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

        // 각 플레이어의 기존 점수를 불러오기
        LoadPlayerScore(player1Name);
        LoadPlayerScore(player2Name);

        LoadScores(); // 점수 데이터 로드

        StateManager.Instance.OnStateChanged += HandleOnStateChanged;

    }

    private void HandleOnStateChanged(StateManager.GameState gameState)
    {
        switch (gameState)
        {
            case StateManager.GameState.Start:
                break;
            case StateManager.GameState.GameScene:
                break;
            case StateManager.GameState.Pause:
                break;
            case StateManager.GameState.Win:
            case StateManager.GameState.Lose:
                // 각 플레이어의 점수 저장
                SaveScore(player1Name);
                SaveScore(player2Name);
                break;
        }
    }

    public void SetBrickManager(BrickManager brick)
    {
        brickManager = brick;
        brickManager.OnBrickBroken += HandleBrickBroken;
    }

    private void HandleBrickBroken(Brick brick)
    {
        // 예시로 현재는 Player1에게만 점수를 추가합니다.
        // 실제로는 어떤 플레이어가 벽돌을 깼는지에 따라 로직을 수정해야 합니다.
        AddScore(player1Name, 10); // 벽돌이 깨질 때마다 10점 추가
        Debug.Log($"AddScore +10 for {player1Name}, Current Score : {GetCurrentScore(player1Name)}");
    }

    public void AddScore(string playerName, int points)
    {
        if (points > 0)
        {
            if (!playerScores.ContainsKey(playerName))
            {
                playerScores[playerName] = 0;
            }

            playerScores[playerName] += points;
            OnScoreUpdate?.Invoke(playerName, playerScores[playerName]);
        }
    }

    public int GetCurrentScore(string playerName)
    {
        if (playerScores.ContainsKey(playerName))
        {
            return playerScores[playerName];
        }
        return 0;
    }

    private void LoadPlayerScore(string playerName)
    {
        // 기존에 동일한 playerName을 가진 점수가 있는지 확인
        ScoreData existingScore = scoreList.Find(score => score.playerName == playerName);

        if (existingScore.playerName != null)
        {
            // 기존 점수를 로드하여 딕셔너리에 저장
            playerScores[playerName] = existingScore.score;
            Debug.Log($"Loaded score for {playerName}: {existingScore.score}");
        }
        else
        {
            // 점수가 없으면 0으로 초기화
            playerScores[playerName] = 0;
            Debug.Log($"No existing score for {playerName}, starting at 0");
        }
    }

    public void SaveScore(string playerName)
    {
        // 새로운 점수 데이터를 생성
        ScoreData newScore = new ScoreData
        {
            score = GetCurrentScore(playerName),
            playerName = playerName,
            date = DateTime.Now.ToString("yyyy-MM-dd")
        };

        // 기존에 동일한 playerName을 가진 점수가 있는지 확인
        int existingIndex = scoreList.FindIndex(score => score.playerName == playerName);

        if (existingIndex >= 0)
        {
            // 같은 이름이 이미 있으면, 기존 점수를 업데이트
            scoreList[existingIndex] = newScore;
            Debug.Log($"Score updated for {playerName}: {newScore.score} on {newScore.date}");
        }
        else
        {
            // 같은 이름이 없으면 새 점수를 추가
            scoreList.Add(newScore);
            Debug.Log($"New score saved: {newScore.score} by {newScore.playerName} on {newScore.date}");
        }

        // 점수를 내림차순으로 정렬 (점수가 높을수록 좋은 순서)
        scoreList.Sort((a, b) => b.score.CompareTo(a.score));

        // 리스트를 JSON으로 직렬화
        string json = JsonConvert.SerializeObject(scoreList, Formatting.Indented);
        File.WriteAllText(filePath, json); // JSON 데이터를 파일에 저장
    }

    public void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // 파일에서 JSON 데이터 읽기
            scoreList = JsonConvert.DeserializeObject<List<ScoreData>>(json); // JSON 데이터를 List<ScoreData>로 변환
        }
    }

    public List<ScoreData> GetTopScores(int count = 3)
    {
        // 상위 count개의 점수 반환
        return scoreList.GetRange(0, Mathf.Min(count, scoreList.Count));
    }
}
