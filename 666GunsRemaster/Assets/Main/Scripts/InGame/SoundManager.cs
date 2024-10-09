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

    // 배경음과 효과음 인덱스를 반응형으로 관리
    private ReactiveProperty<int> bgmIndexToPlay = new ReactiveProperty<int>(-1);
    private ReactiveProperty<int> effectIndexToPlay = new ReactiveProperty<int>(-1);

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

    // 싱글턴 패턴 적용
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 구독
        }
        else
        {
            Destroy(gameObject);
        }

        // ReactiveProperty 구독: bgmIndexToPlay 값이 변할 때마다 해당 BGM 재생
        bgmIndexToPlay
            .Where(index => index >= 0 && index < bgmSources.Count) // 유효한 인덱스 체크
            .Subscribe(index => PlayBGMSound(index))
            .AddTo(this); // 수명 관리

        // ReactiveProperty 구독: effectIndexToPlay 값이 변할 때마다 해당 효과음 재생
        effectIndexToPlay
            .Where(index => index >= 0 && index < effectSources.Count) // 유효한 인덱스 체크
            .Subscribe(index => PlayEffectSound(index))
            .AddTo(this); // 수명 관리
    }

    // 씬이 로드되면, 저장된 인덱스의 BGM과 효과음을 자동 재생
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되었을 때 특별한 처리가 필요하다면 추가할 수 있음
    }

    public void PlayEffectSound(int index)
    {
        //Update문에 호출 시 중복재생을 막기 위함.
        if (index >= 0 && index < effectSources.Count && effectSources[index] != null)
        {
            effectSources[index].Play();
        }
    }

    public void PlayEffectSoundOnce(int index)
    {
        //Update문에 호출 시 중복재생을 막기 위함.
        if (index >= 0 && index < effectSources.Count && effectSources[index] != null && !effectSources[index].isPlaying)
        {
            effectSources[index].Play();
        }
    }

    public void PlayBGMSound(int index)
    {
        //Update문에 호출 시 중복재생을 막기 위함.
        if (index >= 0 && index < bgmSources.Count && bgmSources[index] != null)
        {
            bgmSources[index].Play();
        }
    }

    public void PlayBGMSoundOnce(int index)
    {
        //Update문에 호출 시 중복재생을 막기 위함.
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

    // ReactiveProperty를 통해 씬 변경 시 재생할 배경음악 인덱스 설정
    public void SetBGMToPlayOnSceneLoad(int index)
    {
        bgmIndexToPlay.Value = index; // 값 변경
    }

    // ReactiveProperty를 통해 씬 변경 시 재생할 효과음 인덱스 설정
    public void SetEffectToPlayOnSceneLoad(int index)
    {
        effectIndexToPlay.Value = index; // 값 변경
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
