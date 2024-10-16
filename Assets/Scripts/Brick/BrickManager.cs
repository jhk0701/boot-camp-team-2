using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BrickManager : MonoBehaviour
{
    [SerializeField] Color[] brickColors;
    // 벽돌 배치 데이터
    [SerializeField] BrickPlacement placement;
    // 벽돌 프리팹
    [SerializeField] Brick prefabBrick;
    public int CurrentCount { get; private set; }

    public event Action OnAllBrickBroken;


    void Start()
    {
        Generate();
        
        OnAllBrickBroken += GameManager.Instance.GameWin;

        // TODO : remove temp code
        OnAllBrickBroken += GetComponent<DummyGameScene>().OpenEndPanel;
    }

    public void Generate()
    {
        for (int i = 0; i < placement.datas.Length; i++)
        {
            Brick b = Instantiate(prefabBrick, transform);
                                    
            b.Initialize(placement.datas[i], brickColors[Random.Range(0, brickColors.Length)]);
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
