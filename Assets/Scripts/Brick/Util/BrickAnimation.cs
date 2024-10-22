using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAnimation : MonoBehaviour
{
    Animator animator;

    readonly int HIT = Animator.StringToHash("Hit");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        GetComponent<Brick>().OnBrickHit += ()=>{animator.SetTrigger(HIT);};
    }
    
}
