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

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour, IBreakable
{    
    public BrickType type;
    [SerializeField] int durability;
    public int Durability
    { 
        get { return durability; }
        protected set
        {
            if (Durability == 0)
                return;

            durability = value;

            if (Durability <= 0)
                Break();
        } 
    }


    SpriteRenderer sprite;
    Collider2D collider;

    public event Action OnBrickBroken;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Durability = durability;
    }

    public void Initialize(Vector2 pos, Color col)
    {
        sprite.color = col;
        transform.position = pos;
    }

    /// <summary>
    /// 벽돌 체력 깎는 메서드
    /// </summary>
    public virtual void Hit()
    {
        if(type.Equals(BrickType.Unbreakable)) 
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
