using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour
{
    public Text LevelAndStageText;
    public Text player1ScoreText;
    public Text player2ScoreText;

    public GameManager gameManager;

    //초기 셋팅
    private void Start()
    {
        gameManager = GameManager.Instance;


        int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
        int currentStage = GameManager.Instance.LevelManager.SelectedStage;

        LevelAndStageText.text = $"Level: {currentLevel + 1} - Stage: {currentStage + 1}";

        string player1Name = ScoreManager.Instance.player1Name;

        int player1CurrentScore = ScoreManager.Instance.GetCurrentScore(player1Name);

        int player1HighScore = ScoreManager.Instance.GetHighScore(player1Name);

        player1ScoreText.text = $"Player 1 Score: {player1CurrentScore} (High: {player1HighScore})";
        
        if(gameManager.gameMode == GameManager.GameMode.Multi)
        {
            string player2Name = ScoreManager.Instance.player2Name;
            int player2CurrentScore = ScoreManager.Instance.GetCurrentScore(player2Name);
            int player2HighScore = ScoreManager.Instance.GetHighScore(player2Name);
            player2ScoreText.text = $"Player 2 Score: {player2CurrentScore} (High: {player2HighScore})";
        }

    }

}
