using UnityEngine;

public class ItemSlowDownTime : Item
{
    PaddleController paddle;

    protected override void Use()
    {
        if (!Initialize())
            return;

        UtilItemEffect utilEffect = itemEffect as UtilItemEffect;

        Time.timeScale = 1f * utilEffect.effectValue;
        paddle = collidedObject.GetComponent<PaddleController>();
        if(paddle != null)
        {       
            paddle.Speed /= utilEffect.effectValue;
        }

    }

    public override void EndEffect(ItemEffect effect)
    {
        if(effect != itemEffect) 
            return;

        Time.timeScale = 1f;

        paddle.Speed *= (itemEffect as UtilItemEffect).effectValue;

        Destroy(gameObject);
    }
}