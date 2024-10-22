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
            Destroy(gameObject); // �̹� �ν��Ͻ��� ������ ���� ������ GameManager�� �ı�
        }

        //Ǯ�� �迭�� �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        //�ε��� �ʱ�ȭ
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // Ǯ���� ������Ʈ�� ������ �޼���
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

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // ��Ȱ��ȭ
        //pools.Add(obj);     // Ǯ�� �ٽ� �ֱ�
    }
}