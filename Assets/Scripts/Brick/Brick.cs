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

// ������ ��� : ���� �¾� �μ�����
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

    [SerializeField] Collider2D collider;

    public event Action OnBrickHitted;
    public event Action OnBrickBroken;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Durability = stat.durability;
    }

    /// <summary>
    /// ���� ü�� ��� �޼���
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

        // 1�� �� ����
        Destroy(gameObject, 1f);

        // TODO : �ı� �� ����Ʈ �߰�
    }
}
