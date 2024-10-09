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
        // 슬라이더의 값 변경 시 호출되는 함수 등록
        bgmVolumeSlider.onValueChanged.AddListener(UpdateBgmVolume);
        effectVolumeSlider.onValueChanged.AddListener(UpdateEffectVolume);

        // 초기 텍스트 설정
        UpdateBgmVolume(bgmVolumeSlider.value);
        UpdateEffectVolume(effectVolumeSlider.value);
    }

    // 슬라이더 값에 따라 텍스트 업데이트
    void UpdateBgmVolume(float value)
    {
        bgmVolumeText.text = (value * 100 ).ToString("F0"); // 퍼센트로 표시

        // 여기서 오디오 시스템에 값 적용
        SoundManager.instance.SetbgmVolume(value);
    }

    // 슬라이더 값에 따라 텍스트 업데이트
    void UpdateEffectVolume(float value)
    {
        effectVolumeText.text = (value * 100).ToString("F0"); // 퍼센트로 표시

        // 여기서 오디오 시스템에 값 적용
        SoundManager.instance.SeteffectVolume(value);
    }
}
