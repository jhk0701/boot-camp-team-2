using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrickType
{
    Normal = 0,
    Unbreakable,
    Item
}

[Serializable]
public struct BrickStat
{
    public int durability;
    public BrickType type;
} 

// ������ ��� : ���� �¾� �μ�����
[RequireComponent(typeof(BrickAnimation))]
public class Brick : MonoBehaviour
{    
    BrickManager manager;
    BrickAnimation animationController;

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


    void Awake()
    {
        manager = transform.parent.GetComponent<BrickManager>();
        animationController = GetComponent<BrickAnimation>();
    }

    void Start()
    {
        Durability = stat.durability;
    }

    /// <summary>
    /// ���� ü�� ��� �޼���
    /// </summary>
    public void Hit()
    {
        // OnBrickHitted?.Invoke();
        manager.CallOnBrickHitted(this);
        animationController.Hit();

        if (stat.type.Equals(BrickType.Unbreakable)) 
            return;

        Durability--;
    }

    public void Break()
    {
        GetComponent<Collider2D>().enabled = false;
        // OnBrickBroken?.Invoke();
        manager.CallOnBrickBroken(this);

        // 1�� �� ����
        Destroy(gameObject, 1f);

        // TODO : �ı� �� ����Ʈ �߰�
    }
}
