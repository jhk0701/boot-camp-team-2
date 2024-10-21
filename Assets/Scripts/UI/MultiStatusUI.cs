using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiStatusUI : MonoBehaviour
{
    //공통
    public Text levelStageText;
    public Text timeText;
    
    //플레이어1
    public Text py1ScoreText;
    public Text py1LivesText;

    //플레이어2
    public Text py2ScoreText;
    public Text py2LivesText;

    private string initiallevelStageText = "";
    private float initialTime = 99f;
    
    private int py1InitialScore = 0;
    private int py1InitialLives = 0;

    private int py2InitialScore = 0;
    private int py2InitialLives = 0;


    // Start is called before the first frame update
    void Start()
    {
        InitializeUI();//초기화

        //점수 업데이트
        ScoreManager.Instance.OnUpdateScore += HandleOnScoreUpdate;

        //목숨 업데이트
        GameManager.Instance.OnLifeUpdate += HandleOnLifeUpdate;
        py1LivesText.text = GameManager.Instance.GetLives().ToString();

        //레벨,스테이지 업데이트
        int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
        int currentStage = GameManager.Instance.LevelManager.SelectedStage;

        levelStageText.text = $"{currentLevel + 1}-{currentStage + 1}";
    }

    void OnDisable()
    {
        ScoreManager.Instance.OnUpdateScore -= HandleOnScoreUpdate;
        GameManager.Instance.OnLifeUpdate -= HandleOnLifeUpdate;
    }

    public void InitializeUI()
    {
        UpdateUI(initiallevelStageText, initialTime, py1InitialScore, py1InitialLives, py2InitialScore, py2InitialLives);
    }

    public void UpdateUI(string levelStage, float playTime, int py1score, int py1ives, int py2score, int py2ives)
    {

        levelStageText.text = levelStage;
        timeText.text = playTime.ToString("F2");
        
        py1ScoreText.text = py1score.ToString();
        py1LivesText.text = py1ives.ToString();

        py2ScoreText.text = py1score.ToString();
        py2LivesText.text = py1ives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = TimeManager.Instance.GetElapsedTime().ToString("F2");
    }
    public void HandleOnLifeUpdate(int lives)
    {
        py1LivesText.text = lives.ToString();
    }

    public void HandleOnScoreUpdate(string playerName, int score)
    {
        py1ScoreText.text = score.ToString();
    }
}
