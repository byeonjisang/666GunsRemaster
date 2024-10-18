using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // 풀링할 오브젝트의 프리팹
    [SerializeField] private int poolSize = 10; // 풀에 보관할 오브젝트 개수

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // 미리 오브젝트 생성하여 풀에 보관
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // 비활성화
            pool.Enqueue(obj);    // 풀에 추가
        }
    }

    // 풀에서 오브젝트를 꺼내는 메서드
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);  // 활성화
            return obj;
        }
        else
        {
            // 만약 풀에 더 이상 오브젝트가 없으면 새로 생성
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // 오브젝트를 풀로 반환하는 메서드
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // 비활성화
        pool.Enqueue(obj);     // 풀에 다시 넣기
    }
}