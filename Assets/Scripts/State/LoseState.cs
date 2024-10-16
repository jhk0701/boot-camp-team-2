using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseState : IGameState
{
    private GameManager gameManager;

    public LoseState(GameManager manager)
    {
        this.gameManager = manager;
    }

    public void EnterState()
    {
        //패배 UI 활성화, 게임 멈춤 등
        gameManager.timeManager.StopTimer();
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {
        // UI 비활성화

        // 임시로 로비 시작화면으로 가도록 했음
        SceneManager.LoadScene(0);
    }

    
}
