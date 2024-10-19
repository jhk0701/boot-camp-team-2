using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Stop", 1f);
    }
    
    void Stop()
    {
        Debug.Log("Stop");
        GetComponent<Rigidbody2D>().Sleep();
    }
}
