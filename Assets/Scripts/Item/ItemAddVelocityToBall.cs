using UnityEngine;

public class ItemAddVelocityToBall : Item
{
    public override void Use()
    {
        Debug.Log("ItemAddVelocityToBall used");

        PaddleController paddle = collidedObject.GetComponent<PaddleController>();
        
        if (paddle != null)
        {
            Vector2 velocity = paddle.ballMovement.rigidbody.velocity;
            paddle.ballMovement.rigidbody.velocity = velocity * effectValue;
        }

        EndEffect();
    }

}
