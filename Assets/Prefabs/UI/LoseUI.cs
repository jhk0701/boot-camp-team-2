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
        Debug.Log("�絵�� �Լ��ʿ�");
    }

    public void LoadHome()
    {
        //Start������
        SceneManager.LoadScene("StartScene");
    }
}
