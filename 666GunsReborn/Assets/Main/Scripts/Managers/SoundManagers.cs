using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SoundManagers : Singleton<SoundManagers>
{
    protected override bool IsPersistent => true;

    [Header("Volume")]
    [Range(0, 1)]
    private float masterVolume = 1f;
    [Range(0, 1)]
    private float musicVolume = 1f;
    [Range(0, 1)]
    private float ambienceVolume = 1f;
    [Range(0, 1)]
    private float sfxVolume = 1f;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    private List<EventInstance> eventInstances;

    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;

    protected override void Awake()
    {
        base.Awake();

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Start()
    {
        //InitializedAmbience(FMODEvents.Instance.Ambience);
        //InitializedMusic(FMODEvents.Instance.Music);
    }

    #region Set Volume
    public float[] GetVolume()
    {
        return new float[] { masterVolume, musicVolume, ambienceVolume, sfxVolume };
    }

    public void SetVolume(int index, float value)
    {
        switch (index)
        {
            case 0:
                masterVolume = value;
                masterBus.setVolume(masterVolume);
                break;
            case 1:
                musicVolume = value;
                musicBus.setVolume(musicVolume);
                break;
            case 2:
                ambienceVolume = value;
                ambienceBus.setVolume(ambienceVolume);
                break;
            case 3:
                sfxVolume = value;
                sfxBus.setVolume(sfxVolume);
                break;
            default:
                Debug.LogWarning("Invalid volume index: " + index);
                break;
        }
    }
    #endregion

    #region Ambience
    private void InitializedAmbience(EventReference ambienceEventReference)
    {
        this.ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void ChangeAmbience(Ambience ambience)
    {
        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambienceEventInstance.release();

        ambienceEventInstance = CreateInstance(FMODEvents.Instance.Ambience[(int)ambience]);
    }
    #endregion

    #region Music
    private void InitializedMusic(EventReference MusicEventReference)
    {
        this.musicEventInstance = CreateInstance(MusicEventReference);
        musicEventInstance.start();
    }

    public void ChangeMusic(Music music)
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicEventInstance.release();

        musicEventInstance = CreateInstance(FMODEvents.Instance.Music[(int)music]);
    }
    #endregion

    #region SFX
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    #endregion

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(instance);
        return instance;
    }

    #region Stop all Sounds
    private void ClearUp()
    {
        foreach (EventInstance instance in eventInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }

    private void OnDestroy()
    {
        ClearUp();
    }
    #endregion
}