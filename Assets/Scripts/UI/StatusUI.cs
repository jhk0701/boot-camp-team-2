using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Text levelText;
    public Text timeText;
    public Text scoreText;
    public Text livesText;
    
    // 초기 값 (예시 값으로 초기화)
    private int initialLevel = 5;
    private float initialTime = 99f;
    private int initialScore = 5;
    private int initialLives = 5;


    // Start is called before the first frame update
    void Start()
    {
        InitializeUI();//예시값

        ScoreManager.Instance.OnUpdateScore += HandleOnScoreUpdate;
        //TimeManager.Instance.
    }

    void OnDisable()
    {
        ScoreManager.Instance.OnUpdateScore -= HandleOnScoreUpdate;
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
