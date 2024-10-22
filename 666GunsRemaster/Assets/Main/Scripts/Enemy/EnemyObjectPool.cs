using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool instance = null;

    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 새로 생성된 GameManager는 파괴
        }

        //풀의 배열을 초기화
        pools = new List<GameObject>[prefabs.Length];

        //인덱스 초기화
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // 풀에서 오브젝트를 꺼내는 메서드
    public GameObject GetObject(int index)
    {
        GameObject select = null;

        foreach(GameObject items in pools[index])
        {
            if(!items.activeSelf)
            {
                select = items;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

    // 오브젝트를 풀로 반환하는 메서드
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // 비활성화
        //pools.Add(obj);     // 풀에 다시 넣기
    }
}