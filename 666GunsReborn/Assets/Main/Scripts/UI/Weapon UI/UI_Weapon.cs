using UnityEngine;
using UnityEngine.UI;

public class UI_Weapon : MonoBehaviour
{
    [Header("Weapon UI Elements")]
    [SerializeField]
    private GameObject[] weaponUI = new GameObject[2];

    [SerializeField]
    private Image[] weaponImages = new Image[2];
    [SerializeField]
    private Text[] weaponBulletTexts = new Text[2];

    [SerializeField]
    private Slider[] weaponReloadSlider = new Slider[2];

    private void Awake()
    {
        WeaponUIEvents.OnUpdateBulletUI += UpdateBulletUI;
        WeaponUIEvents.OnUpdateReloadSlider += UpdateReloadSlider;
        WeaponUIEvents.OnUpdateWeaponImage += UpdateWeaponImage;
        WeaponUIEvents.OnSwitchWeaponUI += SwitchWeapon;
    }

#region Update Bullet Count Script
/// <summary>
/// Updates the bullet count UI for the specified weapon index.
/// </summary>
/// <param name="index"></param>
/// <param name="maxMagazine"></param>
/// <param name="currentMagazine"></param>
    public void UpdateBulletUI(int index, int maxMagazine, int currentMagazine)
    {
        weaponBulletTexts[index].text = currentMagazine + " / " + maxMagazine;
    }
    #endregion

#region Update Reload Slider Script
    // 재장전 쿨타임 슬라이더 업데이트
    public void UpdateReloadSlider(int index, float reloadTime, float currentReloadTime)
    {
        weaponReloadSlider[index].value = currentReloadTime / reloadTime;
    }
#endregion

    public void UpdateWeaponImage(Sprite weapon1Sprite, Sprite weapon2Sprite)
    {
        weaponImages[0].sprite = weapon1Sprite;
        weaponImages[1].sprite = weapon2Sprite;
    }

    // 메인 무기와 서브 부기 UI 변경
    public void SwitchWeapon()
    {
        weaponUI[WeaponManager.Instance.CurrentWeaponIndex].transform.SetAsLastSibling();

        // 메인 무기랑 서브 무기 UI를 서로 바꿔줌
        // 위치 변경
        Vector3 subBackgroundPosition = weaponUI[WeaponManager.Instance.CurrentWeaponIndex].transform.position;
        weaponUI[WeaponManager.Instance.CurrentWeaponIndex].transform.position = weaponUI[1 - WeaponManager.Instance.CurrentWeaponIndex].transform.position;
        weaponUI[1 - WeaponManager.Instance.CurrentWeaponIndex].transform.position = subBackgroundPosition;

        // 배경 색상 변경
        Color subBackgroundColor = weaponUI[WeaponManager.Instance.CurrentWeaponIndex].GetComponent<Image>().color;
        weaponUI[WeaponManager.Instance.CurrentWeaponIndex].GetComponent<Image>().color = weaponUI[1 - WeaponManager.Instance.CurrentWeaponIndex].GetComponent<Image>().color;
        weaponUI[1 - WeaponManager.Instance.CurrentWeaponIndex].GetComponent<Image>().color = subBackgroundColor;
    }
}