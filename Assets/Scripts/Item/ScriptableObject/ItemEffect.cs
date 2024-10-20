using System;
using UnityEngine;

public enum ItemTarget
{
    PlayerLife,
    Ball,
    Paddle,
    TimeScale,
}

public enum ApplyingType
{
    Add,
    Multiply
}

[Serializable]
public class Stat
{
    public float speed;
    public int damage;
    public float size;
}

[CreateAssetMenu(fileName = "ItemEffect", menuName ="Item/Item Effect")]
public class ItemEffect : ScriptableObject
{
    public ItemTarget target;
    public ApplyingType applyingType;
    public float effectDuration;

    public GameObject vfxObject;
}