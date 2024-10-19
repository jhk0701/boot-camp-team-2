using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaisePaddleSize : Item
{
    PaddleController paddle;
    float originalSize;

    public override void Use()
    {
        paddle = collidedObject.GetComponent<PaddleController>();
        if(paddle != null)
        {
            originalSize = paddle.Size;
            paddle.Size = originalSize * effectValue;
        }

        Invoke("EndEffect", effectDuration);
    }

    public override void EndEffect()
    {
        paddle.Size = originalSize;
        base.EndEffect();
    }
}
