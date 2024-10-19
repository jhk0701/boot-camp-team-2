using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] Button homeButton;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(Retry);
        homeButton.onClick.AddListener(LoadHome);
    }

    // Update is called once per frame
    public void Retry()
    {
        //다음레벨
        gameObject.SetActive(false);
        //GameManager.Instance.StartGameScene();
        //BrickManager.Instance.Generate();
        //브릭 초기화 함수 필요
        //점수 초기화 필요
    }

    public void LoadHome()
    {
        //Start씬으로
        GameManager.Instance.BackToLobby();
    }
}
