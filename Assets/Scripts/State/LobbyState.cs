using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyState : IGameState
{
    private GameManager gameManager;
    public LobbyState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    public void EnterState()
    {
        SceneManager.LoadScene(0);
        //UI, Sound È°¼ºÈ­
    }
    public void UpdateState()
    {
    }

    public void ExitState()
    {
        gameManager.timeManager.StartTimer();
    }

}
