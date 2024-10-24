﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSelection : MonoBehaviour
{
    [SerializeField] LevelGroup levelGroup;
    [SerializeField] Transform contents;

    public Text LevelAndStageText;
    public Text player1ScoreText;
    public Text player2ScoreText;

    private GameManager gameManger;

    [SerializeField] GameObject Scoreboard;

    void Start()
    {
        SetStages();
    }

    void SetStages()
    {
        LevelManager levelManager = GameManager.Instance.LevelManager;
        gameManger = GameManager.Instance;
        // 레벨과 스테이지 버튼을 설정
        for (int i = 0; i < levelManager.levels.Length; i++)
        {
            LevelGroup group = Instantiate(levelGroup, contents);

            group.levelTitle.text = levelManager.levels[i].levelName;

            // 각 스테이지에 맞는 버튼 설정
            for (int j = 0; j < group.buttons.Length; j++)
            {
                int level = i;
                int stage = j;

                group.buttons[j].onClick.AddListener(() =>
                {
                    DisplayStageScore(level, stage);
                    
                });

                
            }
        }
    }

    //// 각 스테이지의 점수를 ScoreManager에서 가져와 버튼에 텍스트로 설정
    //void SetStageScoreText(Button button, int level, int stage)
    //{
    //    string player1Name = ScoreManager.Instance.player1Name;
    //    string player2Name = ScoreManager.Instance.player2Name;

    //    int player1Score = ScoreManager.Instance.GetCurrentScore(player1Name, level, stage);
    //    int player2Score = ScoreManager.Instance.GetCurrentScore(player2Name, level, stage);
    //}

    void DisplayStageScore(int level, int stage)
    {
        Scoreboard.SetActive(true);

        LevelAndStageText.text = $"Level: {level + 1} - Stage: {stage + 1}";

        string player1Name = ScoreManager.Instance.player1Name;

        int player1HighScore = ScoreManager.Instance.GetHighScore(player1Name, level, stage);

        player1ScoreText.text = $"Player 1 HighScore: {player1HighScore}";

        string player2Name = ScoreManager.Instance.player2Name;
        int player2HighScore = ScoreManager.Instance.GetHighScore(player2Name, level, stage);
        player2ScoreText.text = $"Player 2 HighScore:  {player2HighScore}";

        Button closedButton = GetComponentInChildren<Button>();
        closedButton.onClick.AddListener(() =>
        {
            Scoreboard.SetActive(false);
        });

    }
}
