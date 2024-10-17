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
    BrickManager manager;
    BrickAnimation brickAnimation;
    private string playerName;
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
                Break(playerName);
        } 
    }


    void Awake()
    {
        manager = transform.parent.GetComponent<BrickManager>();
        brickAnimation = GetComponent<BrickAnimation>();
    }

    /// <summary>
    /// 벽돌 체력 깎는 메서드
    /// </summary>
    public void Hit(string playerName)
    {
        this.playerName = playerName;

        manager.CallOnBrickHitted(this);
        brickAnimation.Hit();

        if (type.Equals(BrickType.Unbreak)) 
            return;

        Durability--;
    }

    public void Break(string playerName)
    {
        GetComponent<Collider2D>().enabled = false;
        manager.CallOnBrickBroken(playerName);

        // 1초 뒤 제거
        Destroy(gameObject, 1f);

        // TODO : 파괴 시 이펙트 추가
    }
}
