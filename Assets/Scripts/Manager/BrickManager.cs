using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BrickFactory))]
public class BrickManager : MonoBehaviour
{
    // �׽�Ʈ������ ������ ���ܵ�. ���� �Ҵ��ؼ� �� �� �ֵ���
    [SerializeField] BrickPlacement placement;
    BrickFactory brickFactory;     // ���ο� ���� �߰� => ����
    
    public int CurrentCount { get; private set; }

    public event Action<Brick> OnBrickHitted;
    public event Action<Brick> OnBrickBroken;
    public event Action OnAllBrickBroken;
    
    // ���� ȭ��
    [SerializeField] GameObject gameEnd;



    void Awake()
    {
        brickFactory = GetComponent<BrickFactory>();
        GameManager.Instance.SetBrickManager(this);
    }

    void Start()
    {
        CurrentCount = 0;
        
        Generate();
        
        OnAllBrickBroken += OpenGameEnd;
    }


    // �޾ƿ� �����ͷ� ���� �����
    void Generate()
    {
        if(placement == null)
            placement = GameManager.Instance.levelManager.GetStage();
        
        foreach (PlacementData data in placement.datas)
        {
            Brick b = brickFactory.Create(data);

            if (!b.type.Equals(BrickType.Unbreak))
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

    public void CallOnBrickHitted(Brick brick)
    {
        OnBrickHitted?.Invoke(brick);
    }

    public void CallOnBrickBroken(Brick brick)
    {
        OnBrickBroken?.Invoke(brick);
        CountBrokenBrick();
    }


    public void OpenGameEnd()
    {
        gameEnd.SetActive(true);
    }

    public void BackToStart()
    {
        SceneManager.LoadScene(0);
        // GameManager.Instance.SetState(GameManager.Instance.lobbyState);
    }
    
}
