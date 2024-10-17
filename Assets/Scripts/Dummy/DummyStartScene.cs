using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dummy
{
    public class DummyStartScene : MonoBehaviour
    {
        public void StartGame()
        {
            //GameManager.Instance.SetState(GameManager.Instance.GameSceneState);
            SceneManager.LoadScene(1);
        }
    }

}

