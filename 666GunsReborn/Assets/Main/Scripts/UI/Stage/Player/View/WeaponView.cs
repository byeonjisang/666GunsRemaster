using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image[] iconImage = new Image[2];
    [SerializeField] private Text[] text = new Text[2];
    [SerializeField] private Slider[] reloadSlider = new Slider[2];

    public event Action OnClick;

    public void Start()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }
}
