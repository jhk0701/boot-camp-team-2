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

public class ItemBehaviour : MonoBehaviour
{
    public ItemTarget target;
    public float effectDuration;
    public float effectValue;
    public GameObject effectObject;
}
