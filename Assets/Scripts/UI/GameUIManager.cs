using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Text levelText;
    public Text timeText;
    public Text scoreText;
    public Text livesText;

    // 초기 값 (예시 값으로 초기화)
    private int initialLevel = 1;
    private float initialTime = 0f;
    private int initialScore = 0;
    private int initialLives = 3;

    [SerializeField] Button retryButton;
    [SerializeField] GameObject loseGamePanel;

    void Start()
    {
        InitializeUI();
        loseGamePanel.SetActive(false); // 초기에는 비활성화
        retryButton.onClick.AddListener(RetryGame);
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

    public void ShowLoseGameUI()
    {
        loseGamePanel.SetActive(true); // 게임 오버 UI 활성화
    }

    public void RetryGame()
    {
        // 게임을 재시작하는 로직
        // 예: 현재 씬 재로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
