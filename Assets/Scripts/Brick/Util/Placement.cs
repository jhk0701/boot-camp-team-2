using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Placement : MonoBehaviour
    {
        // [SerializeField] Brick brick;
        // [SerializeField] List<Brick> brickInst;
        [SerializeField] Transform model;
        [SerializeField] BrickPlacement target;

        [ContextMenu("Read Placement")]
        public void Read()
        {

            target.datas = null;
            target.datas = new PlacementData[model.childCount];

            for (int i = 0; i < model.childCount; i++)
            {
                Brick brick = model.GetChild(i).GetComponent<Brick>();
                
                target.datas[i] = new PlacementData()
                {
                    durability = brick.Durability,
                    type = brick.type,
                    position = brick.transform.position,
                    size = brick.transform.localScale
                };
                
                if(target.datas[i].durability == 0)
                    target.datas[i].durability = 1;
            }
        }

    }
}
