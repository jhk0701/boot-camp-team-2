using UnityEngine;
using System.Collections.Generic;

public class BrickFactory : MonoBehaviour
{
    [SerializeField] Transform brickContainer;
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
        return Instantiate(brickDictionary[type], brickContainer);
    }

    public Brick Create(PlacementData data)
    {
        Brick instance = Create(data.type);

        instance.transform.localPosition = data.position;
        instance.transform.localScale = data.size;
        instance.type = data.type;
        instance.Durability = data.durability;

        SpriteRenderer sprite = instance.GetComponentInChildren<SpriteRenderer>();

        if (!data.type.Equals(BrickType.Unbreak))
            sprite.color = brickColors[Random.Range(0, brickColors.Length)];

        return instance;
    }
}