using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSingleOrMluti : MonoBehaviour
{
    public Button SingleBtn;
    public Button MultBtn;

    private void OnEnable()
    {
        SingleBtn.onClick.AddListener(()=>GameManager.Instance.SetSinglePlayMode());
        MultBtn.onClick.AddListener(()=>GameManager.Instance.SetMultiPlayMode());
    }

}
