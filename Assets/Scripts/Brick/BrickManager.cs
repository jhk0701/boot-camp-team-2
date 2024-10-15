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
            
            // 우선 정렬 배치
            // TODO : Manager에서 정렬된 배치를 받는 경우 제거될 수 있음.
            // TODO : Magic Nuber 제거
            Vector2 pos = new Vector2(i % 5 - 2, i / 5 * -0.5f + 4.5f);
            
            // TODO : 완전 자동보단 정해진 색중에서 골라써보기
            Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            
            b.OnBrickBroken += CountBrokenBrick;
            b.Initialize(pos, col);

            if(!b.type.Equals(BrickType.Unbreakable))
                CurrentCount++;
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
