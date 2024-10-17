using UnityEngine;
using System.Collections.Generic;

public class BrickFactory : MonoBehaviour
{
    // 벽돌 생성 기능
    [SerializeField] Color[] brickColors;
    [SerializeField] Brick[] prefabs;
    Dictionary<BrickType, Brick> brickDictionary;

    void Awake()
    {
        brickDictionary = new Dictionary<BrickType, Brick>();

        for (int i = 0; i < prefabs.Length; i++)
            brickDictionary.Add(prefabs[i].stat.type, prefabs[i]);
    }

    public Brick Create(BrickType type)
    {
        return Instantiate(brickDictionary[type], transform);
    }

    public Brick Create(PlacementData data)
    {
        Brick instance = Create(data.stat.type);

        instance.transform.position = data.position;
        instance.transform.localScale = data.size;
        instance.stat = data.stat;

        SpriteRenderer sprite = instance.GetComponentInChildren<SpriteRenderer>();

        if(data.stat.type.Equals(BrickType.Unbreakable))
            sprite.color = Color.gray;
        else
            sprite.color = brickColors[Random.Range(0, brickColors.Length)];

        return instance;
    }
}