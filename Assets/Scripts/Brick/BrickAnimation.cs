using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAnimation : MonoBehaviour
{
    Animator animator;

    readonly int HIT = Animator.StringToHash("Hit");

    public void Hit()
    {
        animator.SetTrigger(HIT);
    }
    
}
