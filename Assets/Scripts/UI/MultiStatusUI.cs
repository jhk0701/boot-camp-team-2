using UnityEngine;
using UnityEngine.UI;

public class MultiStatusUI : MonoBehaviour
{
    // 공통
    public Text levelStageText;
    public Text timeText;

    // 플레이어 1
    public Text py1ScoreText;
    public Text py1LivesText;

    // 플레이어 2
    public Text py2ScoreText;
    public Text py2LivesText;

    private string initiallevelStageText = "";
    private float initialTime = 99f;

    private int py1InitialScore = 0;
    private int py1InitialLives = 0;

    private int py2InitialScore = 0;
    private int py2InitialLives = 0;

    void Start()
    {
        InitializeUI(); // 초기화

        // 점수 업데이트 이벤트 구독
        ScoreManager.Instance.OnUpdateScore += HandleOnScoreUpdate;

        // 라이프 업데이트 이벤트 구독
        GameManager.Instance.OnLifeUpdate += HandleOnLifeUpdate;

        // 초기 라이프 설정
        py1LivesText.text = GameManager.Instance.GetLives(ScoreManager.Instance.player1Name).ToString();
        py1ScoreText.text = ScoreManager.Instance.GetCurrentScore(ScoreManager.Instance.player1Name).ToString();

        if (GameManager.Instance.gameMode == GameManager.GameMode.Multi)
        {
            py2LivesText.gameObject.SetActive(true);
            py2ScoreText.gameObject.SetActive(true);

            py2LivesText.text = GameManager.Instance.GetLives(ScoreManager.Instance.player2Name).ToString();
            py2ScoreText.text = ScoreManager.Instance.GetCurrentScore(ScoreManager.Instance.player2Name).ToString();
        }
        else
        {
            py2LivesText.gameObject.SetActive(false);
            py2ScoreText.gameObject.SetActive(false);
        }

        // 레벨, 스테이지 업데이트
        int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
        int currentStage = GameManager.Instance.LevelManager.SelectedStage;

        levelStageText.text = $"{currentLevel + 1}-{currentStage + 1}";
    }

    public void InitializeUI()
    {
        UpdateUI(initiallevelStageText, initialTime, py1InitialScore, py1InitialLives, py2InitialScore, py2InitialLives);
    }

    public void UpdateUI(string levelStage, float playTime, int py1score, int py1lives, int py2score, int py2lives)
    {
        levelStageText.text = levelStage;
        timeText.text = playTime.ToString("F2");

        py1ScoreText.text = py1score.ToString();
        py1LivesText.text = py1lives.ToString();

        py2ScoreText.text = py2score.ToString();
        py2LivesText.text = py2lives.ToString();
    }

    void Update()
    {
        timeText.text = TimeManager.Instance.GetElapsedTime().ToString("F2");
    }

    public void HandleOnLifeUpdate(string playerName, int lives)
    {
        if (playerName == ScoreManager.Instance.player1Name)
        {
            py1LivesText.text = lives.ToString();
        }
        else if (playerName == ScoreManager.Instance.player2Name)
        {
            py2LivesText.text = lives.ToString();
        }
    }

    public void HandleOnScoreUpdate(string playerName, int score)
    {
        if (playerName == ScoreManager.Instance.player1Name)
        {
            py1ScoreText.text = score.ToString();
        }
        else if (playerName == ScoreManager.Instance.player2Name)
        {
            py2ScoreText.text = score.ToString();
            Debug.Log(score.ToString());
        }
    }

    void OnDisable()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnUpdateScore -= HandleOnScoreUpdate;

        if (GameManager.Instance != null)
            GameManager.Instance.OnLifeUpdate -= HandleOnLifeUpdate;
    }
}
