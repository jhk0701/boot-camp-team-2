using System;
using UnityEngine;

[Serializable]
public class PlacementData
{
    public int durability;
    public BrickType type;
    public Vector2 position;
    public Vector2 size;
}

[CreateAssetMenu(fileName = "BrickPlacement", menuName = "BrickGenerator/BrickPlacement")]
public class BrickPlacement : ScriptableObject
{
    public PlacementData[] datas; 
}