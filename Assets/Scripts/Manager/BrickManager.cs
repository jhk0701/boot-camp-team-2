using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BrickFactory))]
public class BrickManager : MonoBehaviour
{
    // public static BrickManager Instance;
    // GameManager로 이전

    // 테스트용으로 변수를 남겨둠. 직접 할당해서 쓸 수 있도록
    [SerializeField] BrickPlacement placement;
    BrickFactory brickFactory;

    public int CurrentCount { get; private set; }

    public static event Action<Brick> OnBrickHitted;
    public event Action<Brick, string> OnBrickBroken;
    public event Action OnAllBrickBroken;

    [Header("Item list")]
    [SerializeField] Item[] items;

    [Header("Test")]
    [SerializeField] int fixedItemId = -1;


    void Awake()
    {
        brickFactory = GetComponent<BrickFactory>();

        GameManager.Instance.SetBrickManager(this);
    }

    void Start()
    {
        CurrentCount = 0;
        
        Generate();
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
            int itemId = fixedItemId >= 0 ? fixedItemId : UnityEngine.Random.Range(0, items.Length);
            
            instances[id].OnBrickBreak += (Vector3 position, string playerName) => {
                Instantiate(items[itemId], position, Quaternion.identity);
            };
            
            instances.RemoveAt(id);
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

    public void CallOnBrickHitted(Brick brick)
    {
        OnBrickHitted?.Invoke(brick);
    }

    public void CallOnBrickBroken(Brick brick, string playerName)
    {
        OnBrickBroken?.Invoke(brick, playerName);
        Debug.Log($"brik was broken by {playerName}");
        if (!brick.type.Equals(BrickType.Unbreak))
            CountBrokenBrick();
    }

}
