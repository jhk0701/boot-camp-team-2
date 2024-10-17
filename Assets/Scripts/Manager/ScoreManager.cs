using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int score; 
    public string playerName; 
    public string date; 
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreData> scores = new List<ScoreData>(); // 모든 점수 데이터를 리스트로 저장
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 
    private ScoreList scoreList = new ScoreList(); 

    private string filePath; // 점수를 저장할 파일 경로

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

    public void SaveScore(string playerName)
    {
        // 게임 시간 기록을 점수로 저장
        float elapsedTime = TimeManager.Instance.GetElapsedTime();

        ScoreData newScore = new ScoreData
        {
            score = Mathf.FloorToInt(elapsedTime), 
            playerName = playerName, 
            date = System.DateTime.Now.ToString("yyyy-MM-dd") 
        };
        scoreList.scores.Add(newScore); 

        // 점수를 오름차순으로 정렬 (시간이 적을수록 좋은 순서)
        scoreList.scores.Sort((a, b) => a.score.CompareTo(b.score));

        string json = JsonUtility.ToJson(scoreList, true); // 점수 리스트를 JSON으로 변환
        File.WriteAllText(filePath, json); // JSON 데이터를 파일에 저장
    }

    public void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // 파일에서 JSON 데이터 읽기
            scoreList = JsonUtility.FromJson<ScoreList>(json); // JSON 데이터를 ScoreList로 변환
        }
    }

    public List<ScoreData> GetTopScores(int count = 5)
    {
        // 상위 count개의 점수 반환(count 정하기)
        return scoreList.scores.GetRange(0, Mathf.Min(count, scoreList.scores.Count));
    }
}
