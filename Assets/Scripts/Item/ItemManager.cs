using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // public ItemBehaviour[] behaviours;
    // public Dictionary<Item,>

    public Dictionary<PaddleController, List<ItemEffect>> ActiveEffects = new Dictionary<PaddleController, List<ItemEffect>>();

    void Awake()
    {
        
    }
}
