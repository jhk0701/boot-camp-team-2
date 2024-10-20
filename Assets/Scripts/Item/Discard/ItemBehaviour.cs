using UnityEngine;

public enum ItemTarget
{
    Player,
    Ball,
    Paddle
}

public enum SpecType
{
    Speed,
    Damage,
    Size,
}

public enum ApplyingType
{
    Add,
    Multiply,
    Overwrite
}

public class CommonStat
{
    public float speed;
    public float damage;
    public float size;
}

public class ItemBehaviour : MonoBehaviour
{
    public ItemTarget target;
    public SpecType specType;
    public ApplyingType applyingType;
    
    public float effectDuration;
    public float effectValue;
    public GameObject effectObject;
}
