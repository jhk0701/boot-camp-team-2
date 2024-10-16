using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAnimation : MonoBehaviour
{
    readonly int HIT = Animator.StringToHash("Hit");

    Brick brick;
    Animator animator;

    void Awake()
    {
        brick = GetComponent<Brick>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        brick.OnBrickHitted += () => { animator.SetTrigger(HIT); };
    }

    
}
