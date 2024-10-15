using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BrickGenerator : MonoBehaviour
{
    [SerializeField] int count = 10;
    [SerializeField] Brick brick;
    [SerializeField] List<Brick> brickInstances;

    public event Action OnAllBrickBroken;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        for (int i = 0; i < count; i++)
        {
            Brick b = Instantiate(brick, transform);
            brickInstances.Add(b);

            // 우선 랜덤 배치
            // TODO : 미리 작성된 배치를 읽어오는 기능
            b.transform.position = new Vector2(Random.Range(-2, 2), Random.Range(-1, 5) * 0.5f);
        }
    }
}
