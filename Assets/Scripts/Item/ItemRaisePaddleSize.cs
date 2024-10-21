using UnityEngine;

public class ItemRaisePaddleSize : Item
{
    PaddleController paddle;

    protected override void Use()
    {
        if (!Initialize())
            return;

        Debug.Log("ItemRaisePaddleSize used");

        paddle = collidedObject.GetComponent<PaddleController>();

        if(paddle != null)
            paddle.Size *= (itemEffect as PowerUpItemEffect).effectStat.size;
    }

    public override void EndEffect(ItemEffect effect)
    {
        if(effect != itemEffect) 
            return;
        
        Debug.Log("ItemRaisePaddleSize ended");
        paddle.Size /= (itemEffect as PowerUpItemEffect).effectStat.size;

        Destroy(gameObject);
    }
}
