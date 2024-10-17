using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour
{
    public Text LevelAndStageText;
    public Text player1ScoreText;
    public Text player2ScoreText;

    private void Start()
    {
        // 현재 레벨과 스테이지 가져오기
        int currentLevel = GameManager.Instance.levelManager.SelectedLevel;
        int currentStage = GameManager.Instance.levelManager.SelectedStage;

        // 레벨과 스테이지를 텍스트로 표시
        LevelAndStageText.text = $"Level: {currentLevel + 1} - Stage: {currentStage + 1}";

        // 스코어 업데이트 이벤트 구독
        ScoreManager.Instance.OnScoreUpdate += UpdateScoreUI;

        // 플레이어 이름 가져오기
        string player1Name = ScoreManager.Instance.player1Name;
        string player2Name = ScoreManager.Instance.player2Name;

        // 초기 스코어 가져오기
        int player1CurrentScore = ScoreManager.Instance.GetCurrentScore(player1Name);
        int player2CurrentScore = ScoreManager.Instance.GetCurrentScore(player2Name);

        // 최고 스코어 가져오기
        int player1HighScore = ScoreManager.Instance.GetHighScore(player1Name);
        int player2HighScore = ScoreManager.Instance.GetHighScore(player2Name);

        // 스코어 텍스트 업데이트
        player1ScoreText.text = $"Player 1 Score: {player1CurrentScore} (High: {player1HighScore})";
        player2ScoreText.text = $"Player 2 Score: {player2CurrentScore} (High: {player2HighScore})";
    }

    private void UpdateScoreUI(string playerName, int score)
    {
        // 현재 레벨과 스테이지 가져오기
        int currentLevel = GameManager.Instance.levelManager.SelectedLevel;
        int currentStage = GameManager.Instance.levelManager.SelectedStage;

        // 최고 스코어 가져오기
        int highScore = ScoreManager.Instance.GetHighScore(playerName);

        // 스코어 텍스트 업데이트
        if (playerName == ScoreManager.Instance.player1Name)
        {
            player1ScoreText.text = $"Player 1 Score: {score} (High: {highScore})";
        }
        else if (playerName == ScoreManager.Instance.player2Name)
        {
            player2ScoreText.text = $"Player 2 Score: {score} (High: {highScore})";
        }
    }
}
