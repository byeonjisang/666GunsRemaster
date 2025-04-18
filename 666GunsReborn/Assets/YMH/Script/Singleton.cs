using System.Collections;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    public static bool HasInstance => _instance != null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name + "_AutoCreated");
                    _instance = obj.AddComponent<T>();
                }

                // 여기선 DontDestroy 안 해도 됨 (Awake에서 할 거니까)
            }

            return _instance;
        }
    }

    /// <summary>
    /// 싱글톤이 씬 전환 시 유지되어야 하는지 여부 (false면 Destroy됨)
    /// </summary>
    protected virtual bool IsPersistent => false;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            if (IsPersistent)
                DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 싱글톤을 초기화합니다.
    /// </summary>
    protected virtual void InitializeSingleton()
    {
        //게임이 실행중이 아니라면 종료합니다.
        if (!Application.isPlaying)
        {
            return;
        }

        _instance = this as T;
    }
}