using UnityEngine;

public class ItemBreakAnyBlock : Item
{
    public override void Use(GameObject paddle)
    {
        PaddleController paddleController = paddle.GetComponent<PaddleController>();
        if(paddleController != null)
        {
            paddleController.ballMovement.SetInvincibleOn();
        }
    }
}
