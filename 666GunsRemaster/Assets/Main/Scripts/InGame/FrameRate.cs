using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public static FrameRate instance = null;
    private void Awake()
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
    }

    //������ �� ������ 60���� ����
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
