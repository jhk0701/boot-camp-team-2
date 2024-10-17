using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Level
{
    public string levelName;
    public BrickPlacement[] Stages;
}

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    
}
