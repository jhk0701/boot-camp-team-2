using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyGameScene : MonoBehaviour
{

    [SerializeField] GameObject panel;

    public void OpenEndPanel()
    {
        panel.SetActive(true);
    }

    public void BackToStart()
    {
        SceneManager.LoadScene(0);
        //GameManager.Instance.SetState(GameManager.Instance.LobbyState);
    }
    
}
