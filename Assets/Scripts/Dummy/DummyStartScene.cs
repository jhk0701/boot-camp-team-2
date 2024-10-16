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
            SceneManager.LoadScene(1);
        }
    }

}

