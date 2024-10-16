using System;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] BrickPlacement placement;     // ���� ��ġ ������
    IBrickFactory brickFactory;     // ���ο� ���� �߰� => ����
    
    // ���� ����
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

    // �޾ƿ� �����ͷ� ���� �����
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

        // ��� ������ �μ���.
        if(CurrentCount == 0)
        {   
            OnAllBrickBroken?.Invoke();
        }
    }
}
