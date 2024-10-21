using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button scoreListButton;

    // Start is called before the first frame update
    void Start()
    {
        //scoreListButton.onClick.AddListener(ScoreList);
    }

    public void ScoreList()
    {
        //scoreList
        Debug.Log("스코어리스트 구현필요");
    }
}
