using UnityEngine;

public class ItemAddLife : Item
{
    public override void Use(GameObject paddle)
    {
        Debug.Log("ItemBreakAnyBlock used");
        GameManager.Instance.AddLife();
    }
}
