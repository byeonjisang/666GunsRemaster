using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int defaultCapacity = 10;
        public int maxSize = 20;
    }

    public List<Pool> pools;
    private Dictionary<string, IObjectPool<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, IObjectPool<GameObject>>();

        foreach (Pool pool in pools)
        {
            poolDictionary[pool.tag] = new ObjectPool<GameObject>(
                () => CreatePooledObject(pool.prefab, pool.tag), // 생성
                obj => obj.SetActive(true),  // 활성화 시
                obj => obj.SetActive(false), // 풀로 반환 시
                obj => Destroy(obj),  // 초과 시 삭제
                true, pool.defaultCapacity, pool.maxSize
            );
        }
    }

    private GameObject CreatePooledObject(GameObject prefab, string tag)
    {
        GameObject obj = Instantiate(prefab);
        obj.name = tag;
        if (obj.TryGetComponent(out IPooledObject pooledObj))
        {
            pooledObj.SetPool(poolDictionary[tag]);
        }
        return obj;
    }

    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"오브젝트 풀에 {tag} 태그가 없습니다.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Get();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Release(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}