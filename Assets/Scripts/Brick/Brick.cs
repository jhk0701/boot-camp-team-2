using System;
using UnityEngine;

public enum BrickType
{
    Normal = 0,
    Unbreak,
    Flow,
    Penalty,
}
// ������ ��� : ���� �¾� �μ�����
[RequireComponent(typeof(BrickAnimation))]
public class Brick : MonoBehaviour
{
    public string playerName;
    public BrickType type;
    [SerializeField] int durability;  
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

    public event Action OnBrickHit;
    public event Action OnBrickBreak;

    /// <summary>
    /// ���� ü�� ��� �޼���
    /// </summary>
    public void Hit(string playerName, bool forceBreak = false)
    {
        this.playerName = playerName;

        BrickManager.Instance.CallOnBrickHitted(this);
        OnBrickHit?.Invoke();
        
        if (forceBreak)
        {
            Durability -= Durability;
            return;
        }
        else if (type.Equals(BrickType.Unbreak)) 
            return;

        Durability--;
    }

    public void Break()
    {
        GetComponent<Collider2D>().enabled = false;

        BrickManager.Instance.CallOnBrickBroken(this);
        OnBrickBreak?.Invoke();

        Destroy(gameObject);
    }
}
