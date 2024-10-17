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
            brickDictionary.Add(prefabs[i].type, prefabs[i]);
    }

    public Brick Create(BrickType type)
    {
        return Instantiate(brickDictionary[type], transform);
    }

    public Brick Create(PlacementData data)
    {
        Brick instance = Create(data.type);

        instance.transform.position = data.position;
        instance.transform.localScale = data.size;
        instance.type = data.type;
        instance.Durability = data.durability;

        SpriteRenderer sprite = instance.GetComponentInChildren<SpriteRenderer>();

        if(data.type.Equals(BrickType.Unbreak))
            sprite.color = Color.gray;
        else
            sprite.color = brickColors[Random.Range(0, brickColors.Length)];

        return instance;
    }
}