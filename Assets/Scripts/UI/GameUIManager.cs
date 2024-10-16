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

    // �ʱ� �� (���� ������ �ʱ�ȭ)
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
        loseGamePanel.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        winGamePanel.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        retryButton.onClick.AddListener(RetryGame);
        foreach (var button in homeButtons)
        {
            button.onClick.AddListener(LoadHome);
        }
        nextLevel.onClick.AddListener(NextLevel);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void NextLevel()
    {
        // ���� ���� ���� �ε����� �����ͼ� ���� ��(����)�� �̵�
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // TODO: ���� ������ �ִ��� Ȯ�� ��, �ε�
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("���� �������� �ε�");
        }
        else
        {
            Debug.Log("�� �̻� ���������� �����ϴ�.");
            // TODO: ���� ���ᳪ ���� ȭ�� ���� ó���� �� ����
        }
    }

}
