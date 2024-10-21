using UnityEngine;

public class ItemAddLife : Item
{
    protected override void Use()
    {
        Debug.Log("ItemAddLife used");
        
        GameManager.Instance.AddLife((int)(itemEffect as UtilItemEffect).effectValue);

        EndEffect(itemEffect);
    }

    public override void EndEffect(ItemEffect effect)
    {
        DestoryItem();
    }
}
