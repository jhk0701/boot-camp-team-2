using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour
{
    public Text LevelAndStageText;
    public Text player1ScoreText;
    public Text player2ScoreText;

    //초기 셋팅
    private void Start()
    {
        int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
        int currentStage = GameManager.Instance.LevelManager.SelectedStage;

        LevelAndStageText.text = $"Level: {currentLevel + 1} - Stage: {currentStage + 1}";

        //ScoreManager.Instance.OnUpdateScore += HandleUpdateScoreUI;

        string player1Name = ScoreManager.Instance.player1Name;
        string player2Name = ScoreManager.Instance.player2Name;

        int player1CurrentScore = ScoreManager.Instance.GetCurrentScore(player1Name);
        int player2CurrentScore = ScoreManager.Instance.GetCurrentScore(player2Name);

        int player1HighScore = ScoreManager.Instance.GetHighScore(player1Name);
        int player2HighScore = ScoreManager.Instance.GetHighScore(player2Name);

        player1ScoreText.text = $"Player 1 Score: {player1CurrentScore} (High: {player1HighScore})";
        player2ScoreText.text = $"Player 2 Score: {player2CurrentScore} (High: {player2HighScore})";
    }

    ////OnScoreUpdate이 될때마다 업데이트 
    //private void HandleUpdateScoreUI(string playerName, int score)
    //{
    //    int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
    //    int currentStage = GameManager.Instance.LevelManager.SelectedStage;

    //    int highScore = ScoreManager.Instance.GetHighScore(playerName);

    //    // 스코어 텍스트 업데이트
    //    if (playerName == ScoreManager.Instance.player1Name)
    //    {
    //        player1ScoreText.text = $"Player 1 Score: {score} (High: {highScore})";
    //    }
    //    else if (playerName == ScoreManager.Instance.player2Name)
    //    {
    //        player2ScoreText.text = $"Player 2 Score: {score} (High: {highScore})";
    //    }
    //}
}
