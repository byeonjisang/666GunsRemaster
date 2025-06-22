using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using System;

public enum SoundType
{
    BGM, WALK, SHOOT
};

[RequireComponent(typeof(AudioSource)),ExecuteInEditMode]
public class SoundManager : Singleton<SoundManager>
{
    protected override bool IsPersistent => true;

    private AudioSource audioSource;
    public SoundList[] audioList;

    public static SoundManager instance;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        //기본적으로 재생할 AudioSource 필요
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundType, float vol = 1f)
    {
        SoundList soundList = instance.audioList[(int)soundType];
        AudioClip clip = soundList.GetClip();

        if (clip != null)
        {
            instance.audioSource.PlayOneShot(clip, vol);
        }
        else
        {
            Debug.LogWarning($"SoundManager: No valid clip found for {soundType}");
        }
    }


#if UNITY_EDITOR
    void OnEnable()
    {
        string[] name = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref audioList, name.Length);

        for (int i = 0; i < name.Length; i++)
        {
            audioList[i].name = name[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] sounds { get => clipList; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] clipList;

    [NonSerialized] public AudioClip selectedClip;

    public AudioClip GetClip()
    {
        if (selectedClip == null && clipList != null && clipList.Length > 0)
        {
            selectedClip = clipList[0]; // 또는 Random.Range(0, clipList.Length)
        }
        return selectedClip;
    }
}

