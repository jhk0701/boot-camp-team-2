using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public string levelName;
    public bool isCleared;
    public BrickPlacement[] Stages;
}

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    
}
