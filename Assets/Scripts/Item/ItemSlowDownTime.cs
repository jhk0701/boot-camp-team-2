using UnityEngine;

public class ItemSlowDownTime : Item
{
    PaddleController paddle;
    public override void Use()
    {
        // 중복 적용 문제
        if(Time.timeScale != 1f)
            return; 

        Time.timeScale = 1f / effectValue;
        paddle = collidedObject.GetComponent<PaddleController>();
        if(paddle != null)
        {       
            paddle.Speed *= effectValue;
        }

        Invoke("EndEffect", effectDuration);
    }

    public override void EndEffect()
    {
        Time.timeScale = 1f;

        paddle.Speed /= effectValue;

        base.EndEffect();
    }
}