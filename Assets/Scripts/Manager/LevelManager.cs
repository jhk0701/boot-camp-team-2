using System;
using UnityEngine;

[Serializable]
public class Level
{
    public string levelName;
    // public bool isCleared;
    public int itemCount;
    public BrickPlacement[] Stages;
}

public class LevelManager : MonoBehaviour
{
    public int SelectedLevel { get; set; }
    public int SelectedStage { get; set; }
    public Level[] levels;

    public BrickPlacement GetStage()
    {
        return levels[SelectedLevel].Stages[SelectedStage];
    }


}
