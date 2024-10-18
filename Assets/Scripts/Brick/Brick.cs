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
        brickAnimation = GetComponent<BrickAnimation>();
    }

    /// <summary>
    /// ���� ü�� ��� �޼���
    /// </summary>
    public void Hit(string playerName)
    {
        this.playerName = playerName;
        
        GameManager.Instance.BrickManager.CallOnBrickHitted(this);
        brickAnimation.Hit();

        if (type.Equals(BrickType.Unbreak)) 
            return;

        Durability--;
    }

    public void Break(string playerName)
    {
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.BrickManager.CallOnBrickBroken(playerName);

        // 1�� �� ����
        Destroy(gameObject, 1f);

        // TODO : �ı� �� ����Ʈ �߰�
    }
}
