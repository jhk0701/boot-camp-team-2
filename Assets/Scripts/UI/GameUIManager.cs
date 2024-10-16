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
    [SerializeField] Button[] homeButtons;
    [SerializeField] Button nextLevel;

    [SerializeField] GameObject loseGamePanel;
    [SerializeField] GameObject winGamePanel;

    void Start()
    {
        InitializeUI();
        loseGamePanel.SetActive(false); // 초기에는 비활성화
        winGamePanel.SetActive(false); // 초기에는 비활성화
        retryButton.onClick.AddListener(RetryGame);
        foreach (var button in homeButtons)
        {
            button.onClick.AddListener(LoadHome);
        }
        nextLevel.onClick.AddListener(NextLevel);
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

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); // WIN UI 활성화
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void NextLevel()
    {
        // 현재 씬의 빌드 인덱스를 가져와서 다음 씬(레벨)로 이동
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // TODO: 다음 레벨이 있는지 확인 후, 로드
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("다음 스테이지 로드");
        }
        else
        {
            Debug.Log("더 이상 스테이지가 없습니다.");
            // TODO: 게임 종료나 엔딩 화면 등을 처리할 수 있음
        }
    }

}
