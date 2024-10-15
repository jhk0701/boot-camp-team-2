using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BrickManager : MonoBehaviour
{
    [SerializeField] int totalCount = 10;
    [SerializeField] Brick brick;
    public int CurrentCount { get; private set; }

    public event Action OnAllBrickBroken;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        // TODO : 미리 작성된 배치를 읽어오는 기능 추가
        for (int i = 0; i < totalCount; i++)
        {
            Brick b = Instantiate(brick, transform);
            b.OnBrickBroken += CountBrokenBrick;

            if(!b.stat.type.Equals(BrickType.Unbreakable))
                CurrentCount++;

            // 우선 랜덤 배치
            b.transform.position = new Vector2(Random.Range(-2, 2), Random.Range(-1, 5) * 0.5f);
        }
    }

    void CountBrokenBrick()
    {
        CurrentCount--;

        // 모든 벽돌이 부서짐.
        if(CurrentCount == 0)
        {   
            OnAllBrickBroken?.Invoke();
        }
    }
}
