using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    private List<AudioSource> bgmSources = new List<AudioSource>();

    [SerializeField]
    private List<AudioSource> effectSources = new List<AudioSource>();

    public void SetbgmVolume(float vol)
    { 
        vol = Mathf.Clamp01(vol); // 볼륨이 0~1 사이의 값으로 제한됨

        foreach (var source in bgmSources)
        {
            if (source != null) // null 체크
            {
                source.volume = vol; // 각 AudioSource의 볼륨을 설정
            }
        }
    }

    public void SeteffectVolume(float vol)
    {
        vol = Mathf.Clamp01(vol); // 볼륨이 0~1 사이의 값으로 제한됨

        foreach (var source in effectSources)
        {
            if (source != null) // null 체크
            {
                source.volume = vol; // 각 AudioSource의 볼륨을 설정
            }
        }
    }

    // 싱글턴
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
