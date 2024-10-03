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
        // �����̴��� �� ���� �� ȣ��Ǵ� �Լ� ���
        volumeSlider.onValueChanged.AddListener(UpdateVolumeText);

        // �ʱ� �ؽ�Ʈ ����
        UpdateVolumeText(volumeSlider.value);
    }

    // �����̴� ���� ���� �ؽ�Ʈ ������Ʈ
    private void UpdateVolumeText(float value)
    {
        volumeText.text = (value * 100 ).ToString("F0"); // �ۼ�Ʈ�� ǥ��
        // ���⼭ ����� �ý��ۿ� �� ���� (��: AudioListener.volume = value;)
    }
}
