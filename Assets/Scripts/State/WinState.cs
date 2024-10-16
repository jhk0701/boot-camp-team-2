using UnityEngine;

public class WinState : IGameState
{
    private GameManager gameManager;

    public WinState(GameManager manager)
    {
        this.gameManager = manager;
    }

    public void EnterState()
    {
        // 승리 UI 활성화, 게임 멈춤 등
        gameManager.timeManager.StopTimer();
        Debug.Log("WinState");
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {
        //UI 비활성화
    }
}
