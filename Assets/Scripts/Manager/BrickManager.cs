using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BrickFactory))]
public class BrickManager : MonoBehaviour
{
    // 테스트용으로 변수를 남겨둠. 직접 할당해서 쓸 수 있도록
    [SerializeField] BrickPlacement placement;
    BrickFactory brickFactory;
    
    public int CurrentCount { get; private set; }

    public event Action<Brick> OnBrickHitted;
    public event Action<string> OnBrickBroken;
    public event Action OnAllBrickBroken;
    
    // 종료 화면
    [SerializeField] GameObject gameEnd;
    [Header("Item list")]
    [SerializeField] Item[] items;

    public static BrickManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        brickFactory = GetComponent<BrickFactory>();
        GameManager.Instance.SetBrickManager(this);
        ScoreManager.Instance.SetBrickManager(this);
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
        List<Brick> instances = new List<Brick>();

        if(placement == null)
            placement = GameManager.Instance.LevelManager.GetStage();
        
        foreach (PlacementData data in placement.datas)
        {
            Brick brick = brickFactory.Create(data);

            if (!brick.type.Equals(BrickType.Unbreak))
            {
                CurrentCount++;
                instances.Add(brick);
            }
        }
        
        int currentLevel = GameManager.Instance.LevelManager.SelectedLevel;
        int itemCount = GameManager.Instance.LevelManager.levels[currentLevel].itemCount;
        
        for (int i = 0; i < itemCount; i++)
        {
            int id = UnityEngine.Random.Range(0, instances.Count);
            int itemId = UnityEngine.Random.Range(0, items.Length);
            Vector3 position = instances[id].transform.position;
            // Debug.Log("Create Item holded brick " + id);
            instances[id].OnBrickBreak += () => {
                Instantiate(items[itemId], position, Quaternion.identity);
            };
            
            instances.RemoveAt(id);
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

    public void CallOnBrickBroken(string playerName)
    {
        OnBrickBroken?.Invoke(playerName);
        CountBrokenBrick();
    }
    //public void CallOnBrickBroken(Brick brick)
    //{
    //    OnBrickBroken?.Invoke(brick);
    //    CountBrokenBrick();
    //}


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
