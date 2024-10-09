using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    private List<AudioSource> bgmSources = new List<AudioSource>();

    [SerializeField]
    private List<AudioSource> effectSources = new List<AudioSource>();

    // ������� ȿ���� �ε����� ���������� ����
    private ReactiveProperty<int> bgmIndexToPlay = new ReactiveProperty<int>(-1);
    private ReactiveProperty<int> effectIndexToPlay = new ReactiveProperty<int>(-1);

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

    // �̱��� ���� ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ����
        }
        else
        {
            Destroy(gameObject);
        }

        // ReactiveProperty ����: bgmIndexToPlay ���� ���� ������ �ش� BGM ���
        bgmIndexToPlay
            .Where(index => index >= 0 && index < bgmSources.Count) // ��ȿ�� �ε��� üũ
            .Subscribe(index => PlayBGMSound(index))
            .AddTo(this); // ���� ����

        // ReactiveProperty ����: effectIndexToPlay ���� ���� ������ �ش� ȿ���� ���
        effectIndexToPlay
            .Where(index => index >= 0 && index < effectSources.Count) // ��ȿ�� �ε��� üũ
            .Subscribe(index => PlayEffectSound(index))
            .AddTo(this); // ���� ����
    }

    // ���� �ε�Ǹ�, ����� �ε����� BGM�� ȿ������ �ڵ� ���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�Ǿ��� �� Ư���� ó���� �ʿ��ϴٸ� �߰��� �� ����
    }

    public void PlayEffectSound(int index)
    {
        //Update���� ȣ�� �� �ߺ������ ���� ����.
        if (index >= 0 && index < effectSources.Count && effectSources[index] != null)
        {
            effectSources[index].Play();
        }
    }

    public void PlayEffectSoundOnce(int index)
    {
        //Update���� ȣ�� �� �ߺ������ ���� ����.
        if (index >= 0 && index < effectSources.Count && effectSources[index] != null && !effectSources[index].isPlaying)
        {
            effectSources[index].Play();
        }
    }

    public void PlayBGMSound(int index)
    {
        //Update���� ȣ�� �� �ߺ������ ���� ����.
        if (index >= 0 && index < bgmSources.Count && bgmSources[index] != null)
        {
            bgmSources[index].Play();
        }
    }

    public void PlayBGMSoundOnce(int index)
    {
        //Update���� ȣ�� �� �ߺ������ ���� ����.
        if (index >= 0 && index < bgmSources.Count && bgmSources[index] != null && !bgmSources[index].isPlaying)
        {
            bgmSources[index].Play();
        }
    }

    public void StopEffectSound(int index)
    {
        if (index >= 0 && index < effectSources.Count && effectSources[index] != null)
        {
            effectSources[index].Stop();
        }
    }

    public void StopBGMSound(int index)
    {
        if (index >= 0 && index < bgmSources.Count && bgmSources[index] != null)
        {
            bgmSources[index].Stop();
        }
    }

    public void StopAllSound()
    {
        for (int i = 0; i < effectSources.Count; i++)
        {
            if (effectSources[i] != null)
            {
                effectSources[i].Stop();
            }
        }

        for (int i = 0; i < bgmSources.Count; i++)
        {
            if (bgmSources[i] != null)
            {
                bgmSources[i].Stop();
            }
        }
    }

    // ReactiveProperty�� ���� �� ���� �� ����� ������� �ε��� ����
    public void SetBGMToPlayOnSceneLoad(int index)
    {
        bgmIndexToPlay.Value = index; // �� ����
    }

    // ReactiveProperty�� ���� �� ���� �� ����� ȿ���� �ε��� ����
    public void SetEffectToPlayOnSceneLoad(int index)
    {
        effectIndexToPlay.Value = index; // �� ����
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
