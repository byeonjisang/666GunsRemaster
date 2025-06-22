using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    protected override bool IsPersistent => true;

    [SerializeField] private AudioClip[] soundClips;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 지정된 인덱스의 사운드를 재생
    /// </summary>
    public void PlaySound(int index, float volume = 1f)
    {
        if (index < 0 || index >= soundClips.Length)
        {
            Debug.LogWarning($"SoundManager: Invalid sound index {index}");
            return;
        }

        AudioClip clip = soundClips[index];
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"SoundManager: Clip at index {index} is null.");
        }
    }
}
