using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button homeButton;

    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        homeButton.onClick.AddListener(LoadHome);
    }

    // Update is called once per frame
    public void NextLevel()
    {
        //다음레벨
        gameObject.SetActive(false);
        Debug.Log("다음레벨로가는 함수 적용필요");
    }

    public void LoadHome()
    {
        //Start씬으로
        GameManager.Instance.BackToLobby();

    }
}
