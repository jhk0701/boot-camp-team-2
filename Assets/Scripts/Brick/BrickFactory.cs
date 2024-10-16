using UnityEngine;
using System.Collections.Generic;

public class BrickFactory : MonoBehaviour, IBrickFactory
{
    [SerializeField] Brick[] prefabs;
    Dictionary<BrickType, Brick> brickDictionary;

    void Awake()
    {
        brickDictionary = new Dictionary<BrickType, Brick>();
    }

    void Start()
    {
        for (int i = 0; i < prefabs.Length; i++)
            brickDictionary.Add(prefabs[i].stat.type, prefabs[i]);
    }

    public Brick Create(BrickType type)
    {
        return Instantiate(brickDictionary[type]);
    }
}