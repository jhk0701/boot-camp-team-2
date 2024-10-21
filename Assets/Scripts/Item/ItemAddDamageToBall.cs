using UnityEngine;

public class ItemAddDamageToBall : Item
{
    PaddleController paddle;
    protected override void Use()
    {
        if (!Initialize())
            return;
        
        Debug.Log("ItemAddDamageToBall used");

        paddle = collidedObject.GetComponent<PaddleController>();

        if(paddle != null)
            paddle.ballMovement.Stat.damage += (itemEffect as PowerUpItemEffect).effectStat.damage;

    }

    public override void EndEffect(ItemEffect effect)
    {
        if(effect != itemEffect) 
            return;

        paddle.ballMovement.Stat.damage -= (itemEffect as PowerUpItemEffect).effectStat.damage;

        Destroy(gameObject);
    }
}
