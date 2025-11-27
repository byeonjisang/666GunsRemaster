using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image[] iconImage = new Image[2];
    [SerializeField] private Text[] text = new Text[2];
    [SerializeField] private Slider[] reloadSlider = new Slider[2];
    [SerializeField] private Text weaponChangeCooldownText;
    
    // 무기 변경 클릭 이벤트
    public event Action OnClick;
    
    public void Start()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());

        weaponChangeCooldownText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 무기 변경 후 UI 업데이트
    /// </summary>
    public void UpdateWeaponUI(int weaponIndex)
    {
        // TODO: 무기 변경하면 앞뒤로 변경되는 효과는 적용
        // 이미지 변경
        Sprite tempIcon = iconImage[weaponIndex].sprite;
        iconImage[weaponIndex].sprite = iconImage[1 - weaponIndex].sprite;
        iconImage[1 - weaponIndex].sprite = tempIcon;

        // 총알 수 변경
        string tempText = text[weaponIndex].text;
        text[weaponIndex].text = text[1 - weaponIndex].text;
        text[1 - weaponIndex].text = tempText;

        // 재장전 슬라이더 변경
        float tempSliderValue = reloadSlider[weaponIndex].value;
        reloadSlider[weaponIndex].value = reloadSlider[1 - weaponIndex].value;
        reloadSlider[1 - weaponIndex].value = tempSliderValue;
    }

    /// <summary>
    /// 무기 변경 쿨타임 UI 업데이트
    /// </summary>
    /// <param name="cooldownTime"></param>
    public void UpdateWeaponChangeCooldown(float cooldownTime)
    {
        if (cooldownTime > 0)
        {
            if(weaponChangeCooldownText.gameObject.activeSelf == false)
                weaponChangeCooldownText.gameObject.SetActive(true);
                
            weaponChangeCooldownText.text = $"{cooldownTime:F0}";
        }
        else
        {
            weaponChangeCooldownText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 무기 총알 UI 업데이트
    /// </summary>
    /// <param name="maxMagazine"></param>
    /// <param name="currentMagazine"></param>
    public void UpdateWeaponBulletUI(int maxMagazine, int currentMagazine)
    {
        text[0].text = $"{currentMagazine} / {maxMagazine}";
    }

    /// <summary>
    /// 무기 재장전 시간표시 UI 업데이트
    /// </summary>
    /// <param name="maxReloadTime"></param>
    /// <param name="currentReloadTime"></param>
    public void UpdateWeaponReloadSlider(float maxReloadTime, float currentReloadTime)
    {
        reloadSlider[0].value = currentReloadTime / maxReloadTime;
    }
}
