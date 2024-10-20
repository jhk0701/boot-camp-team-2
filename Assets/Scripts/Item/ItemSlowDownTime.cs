using UnityEngine;

public class ItemSlowDownTime : Item
{
    PaddleController paddle;

    public override void Use()
    {
        // 중복 적용 문제
        if(Time.timeScale != 1f)
            return; 

        UtilItemEffect utilEffect = itemEffect as UtilItemEffect;

        Time.timeScale = 1f * utilEffect.effectValue;
        paddle = collidedObject.GetComponent<PaddleController>();
        if(paddle != null)
        {       
            paddle.Speed /= utilEffect.effectValue;
        }

        Invoke("EndEffect", utilEffect.effectDuration);
    }

    public override void EndEffect()
    {
        Time.timeScale = 1f;

        paddle.Speed *= (itemEffect as UtilItemEffect).effectValue;

        base.EndEffect();
    }
}