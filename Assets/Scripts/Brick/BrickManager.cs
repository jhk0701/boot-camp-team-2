using System;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] BrickPlacement placement;     // 벽돌 배치 데이터
    IBrickFactory brickFactory;     // 새로운 벽돌 추가 => 위임
    
    // 벽돌 관리
    public int CurrentCount { get; private set; }
    public event Action OnAllBrickBroken;


    void Awake()
    {
        brickFactory = GetComponent<IBrickFactory>();
    }


    void Start()
    {
        CurrentCount = 0;
        
        Generate();
        
        OnAllBrickBroken += GameManager.Instance.GameWin;
        // TODO : remove temp code
        OnAllBrickBroken += GetComponent<DummyGameScene>().OpenEndPanel;
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
}
