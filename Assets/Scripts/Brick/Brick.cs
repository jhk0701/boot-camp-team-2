using System;
using UnityEngine;

public enum BrickType
{
    Normal = 0,
    Unbreak,
    Flow,
    Penalty,
}

[Serializable]
public struct BrickStat
{
} 

// ������ ��� : ���� �¾� �μ�����
[RequireComponent(typeof(BrickAnimation))]
public class Brick : MonoBehaviour
{    
    BrickManager manager;
    BrickAnimation brickAnimation;

    [SerializeField] int durability;
    public BrickType type;
    public int Durability
    { 
        get { return durability; }
        set
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
        brickAnimation = GetComponent<BrickAnimation>();
    }

    /// <summary>
    /// ���� ü�� ��� �޼���
    /// </summary>
    public void Hit()
    {
        manager.CallOnBrickHitted(this); // �Ŵ����� �̺�Ʈ ȣ��

        brickAnimation.Hit();

        if (type.Equals(BrickType.Unbreak)) 
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
