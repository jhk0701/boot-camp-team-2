using UnityEngine;

public class ItemAddVelocityToPaddle : Item
{
    PaddleController paddle;
    protected override void Use()
    {
        if (!Initialize())
        {
            DestoryItem();
            return;
        }
            
        Debug.Log("ItemAddVelocityToPaddle used");

        paddle = collidedObject.GetComponent<PaddleController>();
        
        if (paddle != null)
        {
            // Vector2 velocity = paddle.ballMovement.RigidBody2d.velocity;
            // paddle.ballMovement.RigidBody2d.velocity = velocity * effectValue;

            paddle.Speed += (itemEffect as PowerUpItemEffect).effectStat.speed;
        }
    }

    public override void EndEffect(ItemEffect effect)
    {
        if(effect != itemEffect) 
            return;

        GameManager.Instance.ItemHandler.OnEffectEnded -= EndEffect;

        paddle.Speed -= (itemEffect as PowerUpItemEffect).effectStat.speed;

        DestoryItem();
    }
}
