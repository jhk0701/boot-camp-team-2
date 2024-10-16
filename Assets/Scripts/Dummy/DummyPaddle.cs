using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dummy
{
    public class DummyPaddle : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            float x = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector3.right * x * Time.deltaTime * 5f);
        }
    }
}


