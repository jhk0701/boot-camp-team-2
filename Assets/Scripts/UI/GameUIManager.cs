using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {
        InitializeUI();
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
}
