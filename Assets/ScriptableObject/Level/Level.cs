using UnityEngine;

[System.Serializable]
public struct Stage
{
    public BrickPlacement brickPlacement;
    // Reward ?
}

[CreateAssetMenu(fileName = "Level", menuName = "BrickGenerator/Level")]
public class Level : ScriptableObject
{
    public string name;
    public Stage[] stages;
}