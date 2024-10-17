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

    // �ʱ� �� (���� ������ �ʱ�ȭ)
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

        InitializeUI();//���ð�

        Canvas canvas = FindObjectOfType<Canvas>();
        Instantiate(statusDisplay, canvas.transform);
        Instantiate(loseGamePanel, canvas.transform);
        Instantiate(winGamePanel, canvas.transform);

        statusDisplay.SetActive(false);
        loseGamePanel.SetActive(false);     //LOSE�г� ��Ȱ��ȭ
        winGamePanel.SetActive(false);      //WINE�г� ��Ȱ��ȭ
        
        retryButton.onClick.AddListener(RetryGame);
        foreach (var button in homeButtons)
        {
            button.onClick.AddListener(LoadHome);
        }
        nextLevel.onClick.AddListener(NextLevel);

        //����
        //ScoreManager.OnScoreUpdate += HandleOnScoreUpdate;
        float asdf = TimeManager.Instance.GetElapsedTime();
        Debug.Log($"asdf : {asdf}");
    }

    private void Update()
    {
        //timeText.text = TimeManager.Instance.GetElapsedTime().ToString();
    }

    // UI �ʱ�ȭ �޼���
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
        loseGamePanel.SetActive(true); // ���� ���� UI Ȱ��ȭ
    }

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); // WIN UI Ȱ��ȭ
    }

    public void RetryGame()
    {
        //�ٽ� ����
        loseGamePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadHome()
    {
        //Start������
        statusDisplay.SetActive(false);
        loseGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
    public void NextLevel()
    {
        //��������
        winGamePanel.SetActive(false);
        Debug.Log("���������ΰ��� �Լ� �����ʿ�");
    }

}
