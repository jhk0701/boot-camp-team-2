using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject winGamePanel;
    [SerializeField] GameObject statusDisplay;
    [SerializeField] GameObject loseGamePanel;

    void Start()
    {
        //구독
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;

        Canvas canvas = FindObjectOfType<Canvas>();
        Instantiate(statusDisplay, canvas.transform);
        Instantiate(loseGamePanel, canvas.transform);

        statusDisplay.SetActive(false);
        loseGamePanel.SetActive(false);     //LOSE패널 비활성화
      
        float asdf = TimeManager.Instance.GetElapsedTime();
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
        statusDisplay.SetActive(true); // 게임 오버 UI 활성화
    }

    public void ShowLoseGameUI()
    {
        loseGamePanel.SetActive(true); // 게임 오버 UI 활성화
    }

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); // WIN UI 활성화
    }

    public void ShowStartUI()
    {
        winGamePanel.SetActive(true); // Start UI 활성화
    }
}
