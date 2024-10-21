using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] Transform transFormUI;

    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject winGamePanel;
    [SerializeField] GameObject statusDisplay;
    [SerializeField] GameObject loseGamePanel;
    [SerializeField] GameObject multiStatusDisplay;
    private GameManager gameManager;


    void Start()
    {
        //구독
        gameManager = GameManager.Instance;
        // ScoreManager.Instance.OnShowScoreBoard += ShowLoseGameUI;
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;

        //초기화
        statusDisplay.SetActive(true);
        loseGamePanel.SetActive(false);     
        winGamePanel.SetActive(false);


    }

    void OnDisable()
    {
        StateManager.Instance.OnStateChanged -= HandleOnStateChanged;
    }

    private void HandleOnStateChanged(StateManager.GameState newState)
    {
        switch (newState)
        {
            case StateManager.GameState.GameScene:
                if (gameManager.gameMode == GameManager.GameMode.Single)
                {
                    statusDisplay.SetActive(true);
                    multiStatusDisplay.SetActive(false);
                }
                else if (gameManager.gameMode == GameManager.GameMode.Multi)
                {
                    statusDisplay.SetActive(false);
                    multiStatusDisplay.SetActive(true);
                }
                break;
            case StateManager.GameState.Pause:
                //PauseUI();
                break;
            case StateManager.GameState.Start:
                ShowStartUI();
                break;
            case StateManager.GameState.Win:
                ShowWinGameUI();
                break;
            case StateManager.GameState.Lose:
                ShowLoseGameUI();
                break;
        }
    }

    public void ShowStatusUI()
    {
        statusDisplay.SetActive(true); // ???? ???? UI ????
    }

    public void ShowMultiStatusUI()
    {
        multiStatusDisplay.SetActive(true); // ???? ???? UI ????
    }

    public void ShowLoseGameUI()
    {
        loseGamePanel.SetActive(true); // ???? ???? UI ????
    }

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); // WIN UI ????
    }

    public void ShowStartUI()
    {
        startGamePanel.SetActive(true); // Start UI ????
    }
}
