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
        //����
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;

        Canvas canvas = FindObjectOfType<Canvas>();
        Instantiate(statusDisplay, canvas.transform);
        Instantiate(loseGamePanel, canvas.transform);

        statusDisplay.SetActive(false);
        loseGamePanel.SetActive(false);     //LOSE�г� ��Ȱ��ȭ
      
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
        statusDisplay.SetActive(true); // ���� ���� UI Ȱ��ȭ
    }

    public void ShowLoseGameUI()
    {
        loseGamePanel.SetActive(true); // ���� ���� UI Ȱ��ȭ
    }

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); // WIN UI Ȱ��ȭ
    }

    public void ShowStartUI()
    {
        winGamePanel.SetActive(true); // Start UI Ȱ��ȭ
    }
}
