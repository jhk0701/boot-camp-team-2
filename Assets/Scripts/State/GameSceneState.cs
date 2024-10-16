using UnityEditor;
using UnityEngine;

public class GameSceneState : IGameState
{
    private GameManager gameManager;

    public GameSceneState(GameManager manager)
    {
        this.gameManager = manager;
    }

    public void EnterState()
    {

    }

    public void UpdateState()
    {
        // 일시 정지 버튼
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            gameManager.PauseGame();
        }

        //패배
        if(gameManager.timeManager.CheckRemainTime() || gameManager.GetLifeCount() <= 0)
        {
            gameManager.GameOver();
        }

        // 승리 => brick manager의 이벤트로 위임
        // if (gameManager.GetBrickCount() <=0)
        // {
        //     gameManager.GameWin();
        // }

        //TODO: 아이템 사용
    }

    public void ExitState()
    {

    }
}
