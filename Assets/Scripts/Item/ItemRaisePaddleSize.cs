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
            paddle.Size *= (itemEffect as PowerUpItemEffect).effectStat.size;
        }

        Invoke("EndEffect", itemEffect.effectDuration);
    }

    public override void EndEffect()
    {
        paddle.Size /= (itemEffect as PowerUpItemEffect).effectStat.size;
        base.EndEffect();
    }
}
