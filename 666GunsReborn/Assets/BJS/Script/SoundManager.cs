using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using System;

public enum SoundType
{
    BGM, EFFECT
};

[RequireComponent(typeof(AudioSource)),ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public SoundList[] audioList;

    public static SoundManager instance;

    void Awake()
    {
        //싱글턴
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //기본적으로 재생할 AudioSource 필요
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType soundType, float vol = 1f)
    {
        AudioClip[] clips = instance.audioList[(int)soundType].sounds;
        AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];


        instance.audioSource.PlayOneShot(clip , vol);
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
    public AudioClip[] sounds {  get => clipList; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] clipList;
}
