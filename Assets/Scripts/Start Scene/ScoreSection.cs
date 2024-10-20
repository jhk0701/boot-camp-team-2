using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSelection : MonoBehaviour
{
    [SerializeField] LevelGroup levelGroup;
    [SerializeField] Transform contents;

    [SerializeField] Text LevelText;
    [SerializeField] Text StageText;

    [SerializeField] Text player1NameText;
    [SerializeField] Text player1ScoreText;

    [SerializeField] Text player2NameText;
    [SerializeField] Text player2ScoreText;

    [SerializeField] GameObject Scoreboard;

    void Start()
    {
        SetStages();
    }

    void SetStages()
    {
        LevelManager levelManager = GameManager.Instance.LevelManager;

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

    // 각 스테이지의 점수를 ScoreManager에서 가져와 버튼에 텍스트로 설정
    void SetStageScoreText(Button button, int level, int stage)
    {
        string player1Name = ScoreManager.Instance.player1Name;
        string player2Name = ScoreManager.Instance.player2Name;

        int player1Score = ScoreManager.Instance.GetCurrentScore(player1Name, level, stage);
        int player2Score = ScoreManager.Instance.GetCurrentScore(player2Name, level, stage);

        Text buttonText = button.GetComponentInChildren<Text>();

        //// 버튼 텍스트를 각 스테이지의 플레이어 점수로 설정
        //buttonText.text = $"Level {level + 1}, Stage {stage + 1}\n" +
        //                  $"{player1Name}: {player1Score}\n" +
        //                  $"{player2Name}: {player2Score}";
    }

    void DisplayStageScore(int level, int stage)
    {
        Scoreboard.SetActive(true);

        string player1Name = ScoreManager.Instance.player1Name;
        string player2Name = ScoreManager.Instance.player2Name;

        int player1Score = ScoreManager.Instance.GetCurrentScore(player1Name, level, stage);
        int player2Score = ScoreManager.Instance.GetCurrentScore(player2Name, level, stage);

        LevelText.text = $"Level {level + 1}";
        StageText.text = $"Stage {stage + 1}";

        player1NameText.text = $"{player1Name}";
        player1ScoreText.text = $"{player1Score}";

        player2NameText.text = $"{player2Name}";
        player2ScoreText.text = $"{player1Score}";

        Button closedButton = GetComponentInChildren<Button>();
        closedButton.onClick.AddListener(() =>
        {
            Scoreboard.SetActive(false);
        });

    }
}
