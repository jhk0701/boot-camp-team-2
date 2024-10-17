using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BrickFactory))]
public class BrickManager : MonoBehaviour
{
    [SerializeField] BrickPlacement placement;     // ���� ��ġ ������
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
    }

    void Start()
    {
        CurrentCount = 0;
        
        Generate();
        
        OnAllBrickBroken += GameManager.Instance.GameWin;
        OnAllBrickBroken += OpenGameEnd;
    }


    // �޾ƿ� �����ͷ� ���� �����
    void Generate()
    {
        foreach (PlacementData data in placement.datas)
        {
            Brick b = brickFactory.Create(data);
            
            // b.OnBrickBroken += CountBrokenBrick;

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

    public void CallOnBrickHitted(Brick brick)
    {
        OnBrickHitted?.Invoke(brick);
    }

    public void CallOnBrickBroken(Brick brick)
    {
        OnBrickBroken?.Invoke(brick);
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
