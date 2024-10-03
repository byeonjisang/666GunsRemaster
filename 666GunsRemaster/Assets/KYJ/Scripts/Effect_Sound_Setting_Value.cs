using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Effect_Sound_Value : MonoBehaviour
{
    public Slider volumeSlider; // 슬라이더
    public Text volumeText;     // 볼륨 텍스트

    private void Start()
    {
        // 슬라이더의 값 변경 시 호출되는 함수 등록
        volumeSlider.onValueChanged.AddListener(UpdateVolumeText);

        // 초기 텍스트 설정
        UpdateVolumeText(volumeSlider.value);
    }

    // 슬라이더 값에 따라 텍스트 업데이트
    private void UpdateVolumeText(float value)
    {
        volumeText.text = (value * 100).ToString("F0");
    }
}
