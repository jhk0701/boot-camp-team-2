using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyState : IGameState
{
    private GameManager gameManager;
    public LobbyState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    public void EnterState()
    {
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
