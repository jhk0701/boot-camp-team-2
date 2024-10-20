using UnityEngine;

public class ItemAddDamageToBall : Item
{
    PaddleController paddle;
    int originalDamage = 0;
    public override void Use()
    {
        paddle = collidedObject.GetComponent<PaddleController>();
        if(paddle != null)
        {
            originalDamage = paddle.ballMovement.Stat.damage;
            paddle.ballMovement.Stat.damage += (int)effectValue;
        }

        Invoke("EndEffect", effectDuration);
    }

    public override void EndEffect()
    {
        paddle.ballMovement.Stat.damage = originalDamage;
        base.EndEffect();
    }
}
