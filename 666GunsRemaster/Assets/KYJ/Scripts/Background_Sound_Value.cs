using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Background_Sound_Value : MonoBehaviour
{
    public Slider volumeSlider;
    public Text volumeText;

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
        volumeText.text = (value * 100 ).ToString("F0"); // 퍼센트로 표시
        // 여기서 오디오 시스템에 값 적용 (예: AudioListener.volume = value;)
    }
}
