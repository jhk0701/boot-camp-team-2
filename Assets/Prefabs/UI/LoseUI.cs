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
        Debug.Log("재도전 함수필요");
    }

    public void LoadHome()
    {
        //Start씬으로
        SceneManager.LoadScene("StartScene");
    }
}
