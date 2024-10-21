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

    public List<VFXPool> Pools;  // 각 VFX에 대한 풀 설정 리스트
    private Dictionary<string, List<GameObject>> PoolDictionary; // 풀을 관리하는 딕셔너리

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

    private void Start()
    {
        BrickManager.OnBrickHitted += HandleBrickHit;
        GameManager.Instance.BrickManager.OnBrickBroken += HandleBrickBroken;
        BallMovement.OnPaddleHit += HandlePaddleHit;
        BallMovement.OnWallHit += HandleWallHit;
    }
    // 풀 초기화
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

    // 풀에서 VFX 가져오기
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            //Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objToSpawn = null;

        // 활성화되지 않은 오브젝트 찾기
        for (int i = 0; i < PoolDictionary[tag].Count; i++)
        {
            if (PoolDictionary[tag][i] == null)
            {
                // 파괴된 오브젝트는 풀에서 제거
                PoolDictionary[tag].RemoveAt(i);
                i--;
            }
            else if (!PoolDictionary[tag][i].activeInHierarchy)
            {
                objToSpawn = PoolDictionary[tag][i];
                break;
            }
        }

        // 사용 가능한 오브젝트가 없으면 새로운 오브젝트 생성
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

    // 사용 후 다시 비활성화 (파티클 재생이 완료되면)
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
            // 파티클 시스템이 없을 경우 기본 시간 대기
            yield return new WaitForSeconds(1f);
            obj.SetActive(false);
        }
    }



    // 각 이벤트에 따라 VFX 재생
    private void HandleBrickHit(Brick brick)
    {
        SpawnFromPool("BrickHitVFX", brick.transform.position);
        // Debug.Log("BrickHitVFX");
    }

    private void HandleBrickBroken(Brick brick, string playerName)
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

    private void OnDisable()
    {
        // 이벤트 구독 해제
        BrickManager.OnBrickHitted -= HandleBrickHit;

        if (GameManager.Instance != null && GameManager.Instance.BrickManager != null)
            GameManager.Instance.BrickManager.OnBrickBroken -= HandleBrickBroken;

        BallMovement.OnPaddleHit -= HandlePaddleHit;
        BallMovement.OnWallHit -= HandleWallHit;
    }
}
