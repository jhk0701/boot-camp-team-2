using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [System.Serializable]
    public class VFXPool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<VFXPool> Pools;  // �� VFX�� ���� Ǯ ���� ����Ʈ
    private Dictionary<string, List<GameObject>> PoolDictionary; // Ǯ�� �����ϴ� ��ųʸ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ǯ �ʱ�ȭ
    private void InitializePool()
    {
        PoolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (var pool in Pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);

                obj.transform.parent = this.transform;

                objectPool.Add(obj);
            }

            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Ǯ���� VFX ��������
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            //Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objToSpawn = null;

        // Ȱ��ȭ���� ���� ������Ʈ ã��
        for (int i = 0; i < PoolDictionary[tag].Count; i++)
        {
            if (PoolDictionary[tag][i] == null)
            {
                // �ı��� ������Ʈ�� Ǯ���� ����
                PoolDictionary[tag].RemoveAt(i);
                i--;
            }
            else if (!PoolDictionary[tag][i].activeInHierarchy)
            {
                objToSpawn = PoolDictionary[tag][i];
                break;
            }
        }

        // ��� ������ ������Ʈ�� ������ ���ο� ������Ʈ ����
        if (objToSpawn == null)
        {
            VFXPool pool = Pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                objToSpawn = Instantiate(pool.prefab);
                objToSpawn.SetActive(false);

                objToSpawn.transform.parent = this.transform;

                PoolDictionary[tag].Add(objToSpawn);
            }
            else
            {
                //Debug.LogWarning($"Prefab for tag {tag} not found.");
                return null;
            }
        }

        if (objToSpawn == null)
        {
            //Debug.LogError("Failed to spawn VFX object.");
            return null;
        }

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;

        StartCoroutine(ReturnToPoolAfterDuration(objToSpawn));

        return objToSpawn;
    }

    // ��� �� �ٽ� ��Ȱ��ȭ (��ƼŬ ����� �Ϸ�Ǹ�)
    private IEnumerator ReturnToPoolAfterDuration(GameObject obj)
    {
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            yield return new WaitForSeconds(ps.main.duration + ps.main.startLifetime.constantMax);
            obj.SetActive(false);
        }
        else
        {
            // ��ƼŬ �ý����� ���� ��� �⺻ �ð� ���
            yield return new WaitForSeconds(1f);
            obj.SetActive(false);
        }
    }

    private void OnEnable()
    {
        BrickManager.OnBrickHitted += HandleBrickHit;
        BrickManager.OnBrickBroken += HandleBrickBroken;
        BallMovement.OnPaddleHit += HandlePaddleHit;
        BallMovement.OnWallHit += HandleWallHit;
    }

    // �� �̺�Ʈ�� ���� VFX ���
    private void HandleBrickHit(Brick brick)
    {
        SpawnFromPool("BrickHitVFX", brick.transform.position);
        Debug.Log("BrickHitVFX");
    }

    private void HandleBrickBroken(Brick brick)
    {
        SpawnFromPool("BrickBrokenVFX", brick.transform.position);
    }

    private void HandlePaddleHit(Vector3 position, int playerNumber)
    {
        string playerVfxTag = $"PaddleHitVFX_Player{playerNumber}";
        
        SpawnFromPool(playerVfxTag, position);
    }

    private void HandleWallHit(Vector3 position)
    {
        SpawnFromPool("WallHitVFX", position);
    }
}
