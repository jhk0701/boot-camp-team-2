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
    [SerializeField] GameObject PasuePanel;
    private GameManager gameManager;


    void Start()
    {
        //구독
        gameManager = GameManager.Instance;
        
        StateManager.Instance.OnStateChanged += HandleOnStateChanged;

        //초기화
        //statusDisplay.SetActive(true);
        loseGamePanel.SetActive(false);
        winGamePanel.SetActive(false);

        if (gameManager.gameMode == GameManager.GameMode.Single)
        {
            statusDisplay.SetActive(true);
            multiStatusDisplay.SetActive(false);
        }
        else if (gameManager.gameMode == GameManager.GameMode.Multi)
        {
            statusDisplay.SetActive(false);
            multiStatusDisplay.SetActive(true);
            Debug.Log(multiStatusDisplay.gameObject.activeInHierarchy);
        }

    }

    void OnDisable()
    {
        StateManager.Instance.OnStateChanged -= HandleOnStateChanged;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (TimeManager.Instance.isPlaying)
            {
                TimeManager.Instance.PauseTime();
                ShowPasueUI();
            }
            else
            {
                TimeManager.Instance.ResumeTimer();
                ClosePaseUI();
            }
        }
    }

    private void HandleOnStateChanged(StateManager.GameState newState)
    {
        switch (newState)
        {
            case StateManager.GameState.GameScene:
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
        statusDisplay.SetActive(true); 
    }

    public void ShowMultiStatusUI()
    {
        multiStatusDisplay.SetActive(true); 
    }

    public void ShowLoseGameUI()
    {
        loseGamePanel.SetActive(true); 
    }

    public void ShowWinGameUI()
    {
        winGamePanel.SetActive(true); 
    }

    public void ShowStartUI()
    {

        startGamePanel.SetActive(true); 
    }

    public void ShowPasueUI()
    {
        PasuePanel.SetActive(true);
    }

    public void ClosePaseUI()
    {
        PasuePanel.SetActive(false);
    }
}
