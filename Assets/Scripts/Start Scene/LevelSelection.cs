using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] LevelGroup levelGroup;
    [SerializeField] Transform contents;

    void Start()
    {
        SetStages();
    }

    void SetStages()
    {
        LevelManager levelManager = GameManager.Instance.levelManager;
        for (int i = 0; i < levelManager.levels.Length; i++)
        {
            LevelGroup group = Instantiate(levelGroup, contents);
            
            group.levelTitle.text = levelManager.levels[i].levelName;
            for (int j = 0; j < group.buttons.Length; j++)
            {
                int level = i;
                int stage = j;

                group.buttons[j].onClick.AddListener(()=>
                {
                    levelManager.SelectedLevel = level;
                    levelManager.SelectedStage = stage;

                    GameManager.Instance.SetState(GameManager.Instance.gameSceneState);
                });
            }
        }

    }

}
