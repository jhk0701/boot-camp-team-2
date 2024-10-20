using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaisePaddleSize : Item
{
    PaddleController paddle;

    public override void Use()
    {
        paddle = collidedObject.GetComponent<PaddleController>();
        if(paddle != null)
        {
            paddle.Size *= effectValue;
        }

        Invoke("EndEffect", effectDuration);
    }

    public override void EndEffect()
    {
        paddle.Size /= effectValue;
        base.EndEffect();
    }
}
