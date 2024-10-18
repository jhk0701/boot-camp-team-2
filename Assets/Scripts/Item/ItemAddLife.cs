
using UnityEngine;

public class ItemAddLife : Item
{
    public override void Use()
    {
        GameManager.Instance.AddLife();
    }
}
