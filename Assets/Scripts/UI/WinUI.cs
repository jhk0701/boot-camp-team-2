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
        //��������
        gameObject.SetActive(false);
        Debug.Log("���������ΰ��� �Լ� �����ʿ�");
    }

    public void LoadHome()
    {
        //Start������
        GameManager.Instance.BackToLobby();

    }
}
