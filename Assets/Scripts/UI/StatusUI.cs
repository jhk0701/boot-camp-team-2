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
    public Text stageText;


    private int initialLevel = 0;
    private float initialTime = 99f;
    private int initialScore = 0;
    private int initialLives = 0;
    private int initialStage = 0;


    // Start is called before the first frame update
    void Start()
    {
        InitializeUI();//예시값

        ScoreManager.Instance.OnUpdateScore += HandleOnScoreUpdate;

        GameManager.Instance.OnLifeUpdate += HandleOnLifeUpdate;
        livesText.text = GameManager.Instance.GetLives().ToString();

        int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
        int currentStage = GameManager.Instance.LevelManager.SelectedStage;

        levelText.text = $"{currentLevel + 1}";
        stageText.text = $"{currentStage + 1}";
    }

    void OnDisable()
    {
        ScoreManager.Instance.OnUpdateScore -= HandleOnScoreUpdate;
        GameManager.Instance.OnLifeUpdate -= HandleOnLifeUpdate;
    }

    public void InitializeUI()
    {
        UpdateUI(initialLevel, initialTime, initialScore, initialLives, initialStage);
    }

    public void UpdateUI(int level, float playTime, int score, int lives, int stage)
    {

        levelText.text = level.ToString();
        timeText.text = playTime.ToString("F2");
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
        stageText.text = stage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = TimeManager.Instance.GetElapsedTime().ToString("F2");
    }
    public void HandleOnLifeUpdate(int lives)
    {
        livesText.text = lives.ToString();
    }

    public void HandleOnScoreUpdate(string playerName, int score)
    {
        scoreText.text = score.ToString();
    }
}
