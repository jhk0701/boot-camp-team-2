using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BrickFactory))]
public class BrickManager : MonoBehaviour
{
    // 테스트용으로 변수를 남겨둠. 직접 할당해서 쓸 수 있도록
    [SerializeField] BrickPlacement placement;
    BrickFactory brickFactory;     // 새로운 벽돌 추가 => 위임
    
    public int CurrentCount { get; private set; }

    public event Action<Brick> OnBrickHitted;
    public event Action<Brick> OnBrickBroken;
    public event Action OnAllBrickBroken;
    
    // 종료 화면
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


    // 받아온 데이터로 벽돌 만들기
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

        // 모든 벽돌이 부서짐.
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
