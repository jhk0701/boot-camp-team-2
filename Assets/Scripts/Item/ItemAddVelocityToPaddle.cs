using UnityEngine;

public class ItemAddVelocityToPaddle : Item
{
    public override void Use()
    {
        Debug.Log("ItemAddVelocityToPaddle used");

        PaddleController paddle = collidedObject.GetComponent<PaddleController>();
        
        if (paddle != null)
        {
            Vector2 velocity = paddle.ballMovement.RigidBody2d.velocity;
            paddle.ballMovement.RigidBody2d.velocity = velocity * effectValue;
        }

        EndEffect();
    }

}
