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
        //��������
        gameObject.SetActive(false);
        //GameManager.Instance.StartGameScene();
        //BrickManager.Instance.Generate();
        //�긯 �ʱ�ȭ �Լ� �ʿ�
        //���� �ʱ�ȭ �ʿ�
    }

    public void LoadHome()
    {
        //Start������
        GameManager.Instance.BackToLobby();
    }
}
