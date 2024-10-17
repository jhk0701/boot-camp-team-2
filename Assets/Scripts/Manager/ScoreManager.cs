using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json; // Newtonsoft.Json 네임스페이스 추가
//using System.Xml;

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
    private List<ScoreData> scoreList = new List<ScoreData>(); // 리스트로 바로 사용

    private string filePath;

    public event Action<int> OnScoreUpdate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Application.persistentDataPath + "/scores.json"; // 점수 데이터 파일 경로 설정
            LoadScores(); // 점수 데이터 로드
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;

        // 벽돌 이벤트 구독
        BrickManager brickManager = FindObjectOfType<BrickManager>();
        if (brickManager != null)
        {
            //brickManager.OnBrickBroken += HandleBrickBroken;
        }
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
                SaveScore("PlayerName"); // TODO: 임시 이름
                break;
        }
    }

    private void HandleBrickBroken()
    {
        AddScore(10); // 벽돌이 깨질 때마다 10점 추가 (예시로 10점 추가)
    }

    public void AddScore(int points)
    {
        if (points > 0)
        {
            int newScore = GetCurrentScore() + points;
            OnScoreUpdate?.Invoke(newScore);
        }
    }

    public int GetCurrentScore()
    {
        if (scoreList.Count > 0)
        {
            return scoreList[scoreList.Count - 1].score;
        }
        return 0;
    }

    public void SaveScore(string playerName)
    {
        // 새로운 점수 데이터를 생성
        ScoreData newScore = new ScoreData
        {
            score = GetCurrentScore(),
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

        // 점수를 오름차순으로 정렬 (점수가 낮을수록 좋은 순서)
        scoreList.Sort((a, b) => a.score.CompareTo(b.score));

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
