using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Effect_Sound_Value : MonoBehaviour
{
    public Slider volumeSlider; // �����̴�
    public Text volumeText;     // ���� �ؽ�Ʈ

    void Start()
    {
        // �����̴��� �� ���� �� ȣ��Ǵ� �Լ� ���
        volumeSlider.onValueChanged.AddListener(UpdateVolumeText);

        // �ʱ� �ؽ�Ʈ ����
        UpdateVolumeText(volumeSlider.value);
    }

    // �����̴� ���� ���� �ؽ�Ʈ ������Ʈ
     void UpdateVolumeText(float value)
    {
        volumeText.text = (value * 100).ToString("F0");
    }
}