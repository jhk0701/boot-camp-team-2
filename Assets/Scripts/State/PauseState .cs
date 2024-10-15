using UnityEngine;

public class PauseState : IGameState
{
    private GameManager gameManager;

    public PauseState(GameManager manager)
    {
        this.gameManager = manager;
    }

    public void EnterState()
    {
        //UI 활성화, 시간 멈추기
        gameManager.timeManager.PauseTimer();
        
    }

    public void UpdateState()
    {
        // 일시 정지 해제
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            gameManager.SetState(new GameSceneState(gameManager));
        }
    }

    public void ExitState()
    {
        // UI 비활성화, 시간 재시작
        gameManager.timeManager.ResumeTimer();
    }
}
