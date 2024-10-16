using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class BrickManager : MonoBehaviour
{
    // ���� ��ġ ���
    [SerializeField] Color[] brickColors;
    // ���� ��ġ ������
    [SerializeField] BrickPlacement placement;
    // ���ο� ���� �߰� => ����
    // [SerializeField] Brick prefabBrick;
    IBrickFactory brickFactory;
    
    // ���� ����
    public int CurrentCount { get; private set; }
    public event Action OnAllBrickBroken;


    void Awake()
    {
        brickFactory = GetComponent<IBrickFactory>();
    }


    void Start()
    {
        Generate();
        
        //OnAllBrickBroken += GameManager.Instance.GameWin;

        // TODO : remove temp code
        OnAllBrickBroken += GetComponent<DummyGameScene>().OpenEndPanel;
    }

    public void Generate()
    {
        for (int i = 0; i < placement.datas.Length; i++)
        {
            Brick b = Instantiate(brickFactory.Create(placement.datas[i].stat.type), transform);
                                    
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

        // ��� ������ �μ���.
        if(CurrentCount == 0)
        {   
            OnAllBrickBroken?.Invoke();
        }
    }
}
