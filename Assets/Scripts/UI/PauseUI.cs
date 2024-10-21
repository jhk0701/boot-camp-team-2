using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseUI : MonoBehaviour
{
    [SerializeField] Button RetryBtn;
    [SerializeField] Button homeBtn;

    // Start is called before the first frame update
    void Start()
    {
        RetryBtn.onClick.AddListener(Retry);
        homeBtn.onClick.AddListener(LoadHome);
    }

    // Update is called once per frame
    public void Retry()
    {
        GameManager.Instance.StartGameScene(); 
    }

    public void LoadHome()
    {
        //Startæ¿¿∏∑Œ
        GameManager.Instance.BackToLobby();

    }
}
