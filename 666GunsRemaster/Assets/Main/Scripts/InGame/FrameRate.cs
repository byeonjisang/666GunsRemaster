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
            Destroy(gameObject); // 이미 인스턴스가 있으면 새로 생성된 GameManager는 파괴
        }
    }

    //시작할 때 프레임 60으로 고정
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
