using System;
using UnityEngine;

public enum BrickType
{
    Normal = 0,
    Unbreak,
    Flow,
    Penalty,
}
// 벽돌의 기능 : 공에 맞아 부서지기
[RequireComponent(typeof(BrickAnimation))]
public class Brick : MonoBehaviour
{
    public string PlayerName;
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
    public event Action<Vector3, string> OnBrickBreak;

    void Start()
    {
        OnBrickHit += () => 
        { 
            GameManager.Instance.soundManager.PlaySfx(SfxType.BrickHit); 
        };
    }

    /// <summary>
    /// 벽돌 체력 깎는 메서드
    /// </summary>
    public void Hit (string playerName, int damage = 1, bool forceBreak = false)
    {
        this.PlayerName = playerName;

        GameManager.Instance.BrickManager.CallOnBrickHitted(this);
        OnBrickHit?.Invoke();
        
        if (forceBreak)
        {
            Durability -= Durability;
            return;
        }
        else if (type.Equals(BrickType.Unbreak))
            return;

        Durability -= damage;
    }

    public void Break()
    {
        GetComponent<Collider2D>().enabled = false;

        GameManager.Instance.BrickManager.CallOnBrickBroken(this, PlayerName);
        OnBrickBreak?.Invoke(transform.position, PlayerName);

        Destroy(gameObject);
    }
}
