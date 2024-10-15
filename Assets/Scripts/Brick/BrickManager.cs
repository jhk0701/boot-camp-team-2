using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BrickManager : MonoBehaviour
{
    [SerializeField] BrickPlacement placement;
    [SerializeField] Brick brick;

    public int CurrentCount { get; private set; }

    public event Action OnAllBrickBroken;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        for (int i = 0; i < placement.datas.Length; i++)
        {
            Brick b = Instantiate(brick, transform);
            
            // TODO : 완전 자동보단 정해진 색중에서 골라써보기
            Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                        
            b.Initialize(placement.datas[i], col);
            b.OnBrickBroken += CountBrokenBrick;

            if(!b.Stat.type.Equals(BrickType.Unbreakable))
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
