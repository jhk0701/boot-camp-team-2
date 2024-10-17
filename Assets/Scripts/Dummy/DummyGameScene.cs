using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyGameScene : MonoBehaviour
{

    [SerializeField] GameObject panel;

    public void OpenEndPanel()
    {
        panel.SetActive(true);
    }

    public void BackToStart()
    {
        GameManager.Instance.SetState(GameManager.Instance.lobbyState);
    }
    
}
