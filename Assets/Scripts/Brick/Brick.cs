using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BrickType
{
    Unbreakable = -1,
    Normal = 0,
}

[Serializable]
public class BrickStat
{
    public int durability;
    public BrickType type;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
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

    SpriteRenderer sprite;
    Collider2D collider;


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Durability = stat.durability;
        // TODO : 완전 자동보단 정해진 색중에서 골라써보기
        sprite.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    /// <summary>
    /// 벽돌 체력 깎는 메서드
    /// </summary>
    public virtual void Hit()
    {
        if(stat.type.Equals(BrickType.Unbreakable)) 
            return;

        Durability--;
    }

    public virtual void Break()
    {
        collider.enabled = false;
        Destroy(gameObject, 3f);
        // TODO : 파괴 시 이펙트 추가
    }
}
