using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Placement : MonoBehaviour
    {
        [SerializeField] Brick brick;
        [SerializeField] List<Brick> brickInst;
        [SerializeField] BrickPlacement target;

        [ContextMenu("Read Placement")]
        public void Read()
        {
            target.datas = null;
            target.datas = new PlacementData[brickInst.Count];
            for (int i = 0; i < brickInst.Count; i++)
            {
                target.datas[i] = new PlacementData()
                {
                    stat = brickInst[i].stat,
                    position = brickInst[i].transform.position,
                    size = brickInst[i].transform.localScale
                };
                
                if(target.datas[i].stat.durability == 0)
                    target.datas[i].stat.durability = 1;
            }
        }

        [ContextMenu("Place")]
        public void Place()
        {
            Clear();

            model1();
        }

        void Clear()
        {
            GameObject dest = new GameObject();
            for (int i = 0; i < brickInst.Count; i++)
            {
                brickInst[i].transform.SetParent(dest.transform);
            }

            DestroyImmediate(dest);
            brickInst.Clear();
        }

        void model1()
        {
             for (int i = 0; i < 30; i++)
            {
                Brick b = Instantiate(brick, transform);
                
                float x = i % 5 - 2f + ((i / 5) % 2 == 0 ? -0.25f : 0.25f);
                float y = (i / 5 * -0.5f) + 4.5f;
                Vector2 pos = new Vector2(x, y);
                Vector2 size = new Vector2(1f, 0.5f);
                
                Color col = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                b.Initialize(pos, size, new BrickStat { durability = 1, type = BrickType.Normal }, col);

                brickInst.Add(b);
            }
        }
    }
}
