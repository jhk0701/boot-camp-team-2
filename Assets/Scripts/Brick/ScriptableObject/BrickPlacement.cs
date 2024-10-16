using System;
using UnityEngine;

[Serializable]
public struct PlacementData
{
    public BrickStat stat;
    public Vector2 position;
    public Vector2 size;
}

[CreateAssetMenu(fileName = "BrickPlacement", menuName = "BrickGenerator/BrickPlacement")]
public class BrickPlacement : ScriptableObject
{
    public PlacementData[] datas; 
}