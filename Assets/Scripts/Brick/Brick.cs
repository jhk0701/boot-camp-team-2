using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrickType
{
    Normal = 0,
    Unbreakable,
    Item,
}

[Serializable]
public struct BrickStat
{
    public int durability;
    public BrickType type;
} 


public class Brick : MonoBehaviour, IBreakable
{
    public BrickStat stat;
    [SerializeField] int durability = 1;
    public int Durability
    { 
        get { return durability; }
        protected set
        {
            if (durability == 0)
                return;

            durability = value;

            if (Durability <= 0)
                Break();
        } 
    }

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Collider2D collider;

    public event Action OnBrickHitted;
    public event Action OnBrickBroken;

    void Awake()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>();

        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Durability = stat.durability;
    }

    public void Initialize(Vector2 pos, Vector2 size, BrickStat brickStat, Color col)
    {
        transform.position = pos;
        transform.localScale = size;
        stat = brickStat;

        if(stat.type.Equals(BrickType.Unbreakable))
            sprite.color = Color.gray;
        else
            sprite.color = col;
    }

    public void Initialize(PlacementData data, Color col)
    {
        Initialize(data.position, data.size, data.stat, col);
    }


    /// <summary>
    /// 벽돌 체력 깎는 메서드
    /// </summary>
    public virtual void Hit()
    {
        OnBrickHitted?.Invoke();

        if (stat.type.Equals(BrickType.Unbreakable)) 
            return;

        Durability--;
    }

    public virtual void Break()
    {
        collider.enabled = false;
        OnBrickBroken?.Invoke();

        // 1초 뒤 제거
        Destroy(gameObject, 1f);

        // TODO : 파괴 시 이펙트 추가
    }
}
