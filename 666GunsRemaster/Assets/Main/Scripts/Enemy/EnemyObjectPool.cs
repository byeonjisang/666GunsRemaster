using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Ǯ���� ������Ʈ�� ������
    [SerializeField] private int poolSize = 10; // Ǯ�� ������ ������Ʈ ����

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // �̸� ������Ʈ �����Ͽ� Ǯ�� ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); // ��Ȱ��ȭ
            pool.Enqueue(obj);    // Ǯ�� �߰�
        }
    }

    // Ǯ���� ������Ʈ�� ������ �޼���
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);  // Ȱ��ȭ
            return obj;
        }
        else
        {
            // ���� Ǯ�� �� �̻� ������Ʈ�� ������ ���� ����
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // ��Ȱ��ȭ
        pool.Enqueue(obj);     // Ǯ�� �ٽ� �ֱ�
    }
}