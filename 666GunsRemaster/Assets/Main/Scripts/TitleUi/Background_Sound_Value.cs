using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Background_Sound_Value : MonoBehaviour
{
    public Slider bgmVolumeSlider;
    public Slider effectVolumeSlider;
    public Text bgmVolumeText;
    public Text effectVolumeText;

    void Start()
    {
        // �����̴��� �� ���� �� ȣ��Ǵ� �Լ� ���
        bgmVolumeSlider.onValueChanged.AddListener(UpdateBgmVolume);
        effectVolumeSlider.onValueChanged.AddListener(UpdateEffectVolume);

        // �ʱ� �ؽ�Ʈ ����
        UpdateBgmVolume(bgmVolumeSlider.value);
        UpdateEffectVolume(effectVolumeSlider.value);
    }

    // �����̴� ���� ���� �ؽ�Ʈ ������Ʈ
    void UpdateBgmVolume(float value)
    {
        bgmVolumeText.text = (value * 100 ).ToString("F0"); // �ۼ�Ʈ�� ǥ��

        // ���⼭ ����� �ý��ۿ� �� ����
        SoundManager.instance.SetbgmVolume(value);
    }

    // �����̴� ���� ���� �ؽ�Ʈ ������Ʈ
    void UpdateEffectVolume(float value)
    {
        effectVolumeText.text = (value * 100).ToString("F0"); // �ۼ�Ʈ�� ǥ��

        // ���⼭ ����� �ý��ۿ� �� ����
        SoundManager.instance.SeteffectVolume(value);
    }
}
