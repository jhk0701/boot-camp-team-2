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

    void Start()
    {
        //구독
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
                ShowStatusUI();
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
        winGamePanel.SetActive(true); // Start UI ????
    }
}
