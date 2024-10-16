using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    [SerializeField] BrickPlacement placement;     // 벽돌 배치 데이터
    BrickFactory brickFactory;     // 새로운 벽돌 추가 => 위임
    
    public int CurrentCount { get; private set; }
    public event Action OnAllBrickBroken;
    
    // 종료 화면
    [SerializeField] GameObject gameEnd;



    void Awake()
    {
        brickFactory = GetComponent<BrickFactory>();
    }

    void Start()
    {
        CurrentCount = 0;
        
        Generate();
        
        OnAllBrickBroken += GameManager.Instance.GameWin;
        OnAllBrickBroken += OpenGameEnd;
    }


    // 받아온 데이터로 벽돌 만들기
    void Generate()
    {
        foreach (PlacementData data in placement.datas)
        {
            Brick b = brickFactory.Create(data);
            
            b.OnBrickBroken += CountBrokenBrick;

            if (!b.stat.type.Equals(BrickType.Unbreakable))
                CurrentCount++;
        }
    }

    void CountBrokenBrick()
    {
        CurrentCount--;
        Debug.Log($"Current count :{CurrentCount}");

        // 모든 벽돌이 부서짐.
        if(CurrentCount == 0)
        {   
            OnAllBrickBroken?.Invoke();
        }
    }


    public void OpenGameEnd()
    {
        gameEnd.SetActive(true);
    }

    public void BackToStart()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.SetState(GameManager.Instance.lobbyState);
    }
    
}
