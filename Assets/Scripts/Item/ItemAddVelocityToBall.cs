using UnityEngine;

public class ItemAddVelocityToBall : Item
{
    [SerializeField] [Range(1f,2f)] float amount = 1.5f;
    public override void Use(GameObject paddle)
    {
        Debug.Log("ItemAddVelocityToBall used");
        PaddleController paddleController = paddle.GetComponent<PaddleController>();
        if (paddleController != null)
        {
            Vector2 velocity = paddleController.ballMovement.rigidbody.velocity;
            paddleController.ballMovement.rigidbody.velocity = velocity * amount;
        }
    }
}
