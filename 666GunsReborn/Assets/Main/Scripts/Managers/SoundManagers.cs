using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;

public class SoundManagers : Singleton<SoundManagers>
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolue = 1f;
    [Range(0, 1)]
    public float musicVolue = 1f;
    [Range(0, 1)]
    public float ambienceVolue = 1f;
    [Range(0, 1)]
    public float sfxVolue = 1f;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    private List<EventInstance> eventInstances;
    //IsPersistent => true;

    protected override void Awake()
    {
        base.Awake();

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    // 임시 볼륨 설정
    private void Update()
    {   
        masterBus.setVolume(masterVolue);
        musicBus.setVolume(musicVolue);
        ambienceBus.setVolume(ambienceVolue);
        sfxBus.setVolume(sfxVolue);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(instance);
        return instance;
    }

    private void ClaerUp()
    {
        foreach (EventInstance instance in eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }

    private void Oestroy()
    {
        ClaerUp();
    }
}
