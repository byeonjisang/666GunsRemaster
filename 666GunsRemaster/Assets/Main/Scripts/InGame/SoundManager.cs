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
        vol = Mathf.Clamp01(vol); // ������ 0~1 ������ ������ ���ѵ�

        foreach (var source in bgmSources)
        {
            if (source != null) // null üũ
            {
                source.volume = vol; // �� AudioSource�� ������ ����
            }
        }
    }

    public void SeteffectVolume(float vol)
    {
        vol = Mathf.Clamp01(vol); // ������ 0~1 ������ ������ ���ѵ�

        foreach (var source in effectSources)
        {
            if (source != null) // null üũ
            {
                source.volume = vol; // �� AudioSource�� ������ ����
            }
        }
    }

    // �̱���
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
