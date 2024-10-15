using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrickType
{
    Normal,
    Unbreakable
}

[Serializable]
public class BrickStat
{
    [SerializeField] public int durability;
    [SerializeField] public BrickType type;
}

[RequireComponent(typeof(Collider2D))]
public class Brick : MonoBehaviour, IBreakable
{
    public BrickStat stat;

    int durability;
    public int Durability
    { 
        get { return durability; }
        protected set
        {
            if (Durability == 0)
                return;

            Durability = value;
            if(Durability < 0)
            {
                Durability = 0;
                Break();
            }
        } 
    }

    void Start()
    {
        Durability = stat.durability;
    }

    /// <summary>
    /// 벽돌 체력 깎는 메서드
    /// </summary>
    public void Hitted()
    {
        Durability--;
    }

    public void Break()
    {

    }
}
