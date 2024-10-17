using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
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

    [SerializeField] Button retryButton;
    [SerializeField] Button[] homeButtons;
    [SerializeField] Button nextLevel;

    [SerializeField] GameObject statusDisplay;
    [SerializeField] GameObject loseGamePanel;
    [SerializeField] GameObject winGamePanel;

    void Start()
    {

        levelText = statusDisplay.transform.Find("LevelInfo").GetComponent<Text>();
        timeText = statusDisplay.transform.Find("TimeInfo").GetComponent<Text>();
        scoreText = statusDisplay.transform.Find("ScoreInfo").GetComponent<Text>();
        livesText = statusDisplay.transform.Find("LivesInfo").GetComponent<Text>();

        InitializeUI();//예시값

        Canvas canvas = FindObjectOfType<Canvas>();
        Instantiate(statusDisplay, canvas.transform);
        Instantiate(loseGamePanel, canvas.transform);
        Instantiate(winGamePanel, canvas.transform);

        statusDisplay.SetActive(false);
        loseGamePanel.SetActive(false);     //LOSE패널 비활성화
        winGamePanel.SetActive(false);      //WINE패널 비활성화
        
        retryButton.onClick.AddListener(RetryGame);
        foreach (var button in homeButtons)
        {
            button.onClick.AddListener(LoadHome);
        }
        nextLevel.onClick.AddListener(NextLevel);

        //구독
        //ScoreManager.OnScoreUpdate += HandleOnScoreUpdate;
        float asdf = TimeManager.Instance.GetElapsedTime();
        Debug.Log($"asdf : {asdf}");
    }

    private void Update()
    {
        //timeText.text = TimeManager.Instance.GetElapsedTime().ToString();
    }

    // UI 초기화 메서드
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

    public void HandleOnScoreUpdate(int score)
    {
        statusDisplay.SetActive(true);
        scoreText.text = score.ToString();
    }

    public void ShowLoseGameUI()
    {
        loseGamePanel.SetActive(true); // 게임 오버 UI 활성화
    }

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); // WIN UI 활성화
    }

    public void RetryGame()
    {
        //다시 시작
        loseGamePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadHome()
    {
        //Start씬으로
        statusDisplay.SetActive(false);
        loseGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
    public void NextLevel()
    {
        //다음레벨
        winGamePanel.SetActive(false);
        Debug.Log("다음레벨로가는 함수 적용필요");
    }

}
