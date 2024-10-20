using UnityEngine;

public class ItemAddLife : Item
{
    public override void Use()
    {
        Debug.Log("ItemAddLife used");
        
        GameManager.Instance.AddLife((int)(itemEffect as UtilItemEffect).effectValue);

        EndEffect();
    }
}
