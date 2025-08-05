using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private Slider[] sliders;

    private void Awake()
    {
        sliders = GetComponentsInChildren<Slider>();
    }

    private void Start()
    {
        int index = 0;
        float[] volumes = SoundManagers.Instance.GetVolume();
        foreach (Slider slider in sliders)
        {
            slider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, index));
            slider.value = volumes[index];
            index++;
        }
    }

    private void OnSliderValueChanged(float value, int index)
    {
        SoundManagers.Instance.SetVolume(index, value);
    }

    private void OnDestroy()
    {
        foreach (Slider slider in sliders)
        {
            slider.onValueChanged.RemoveAllListeners();
        }
    }
}
