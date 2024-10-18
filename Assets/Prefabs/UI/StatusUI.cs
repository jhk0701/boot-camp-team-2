using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    private Text levelText;
    private Text timeText;
    private Text scoreText;
    private Text livesText;
    
    // 초기 값 (예시 값으로 초기화)
    private int initialLevel = 5;
    private float initialTime = 99f;
    private int initialScore = 5;
    private int initialLives = 5;


    // Start is called before the first frame update
    void Start()
    {
        levelText = gameObject.transform.Find("LevelInfo").GetComponent<Text>();
        timeText = gameObject.transform.Find("TimeInfo").GetComponent<Text>();
        scoreText = gameObject.transform.Find("ScoreInfo").GetComponent<Text>();
        livesText = gameObject.transform.Find("LivesInfo").GetComponent<Text>();

        InitializeUI();//예시값

        ScoreManager.Instance.OnUpdateScore += HandleOnScoreUpdate;
        //TimeManager.Instance.
    }

    public void InitializeUI()
    {
        UpdateUI(initialLevel, initialTime, initialScore, initialLives);
    }

    public void UpdateUI(int level, float playTime, int score, int lives)
    {

        levelText.text = level.ToString();
        timeText.text = playTime.ToString("F2");
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleOnScoreUpdate(string playerName, int score)
    {
        scoreText.text = score.ToString();
    }
}
