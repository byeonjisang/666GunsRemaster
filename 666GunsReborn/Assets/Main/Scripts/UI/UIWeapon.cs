using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
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
        WeaponManager.Instance.OnUpdateBulletUI += UpdateBulletUI;
        WeaponManager.Instance.OnUpdateReloadSlider += UpdateReloadSlider;
        WeaponManager.Instance.OnUpdateWeaponImage += UpdateWeaponImage;
        WeaponManager.Instance.OnSwitchWeapon += SwitchWeapon;
    }

    public void UpdateBulletUI(int index, int maxMagazine, int currentMagazine)
    {
        weaponBulletTexts[index].text = currentMagazine + " / " + maxMagazine;
    }

    public void UpdateReloadSlider(int index, float reloadTime, float currentReloadTime)
    {
        weaponReloadSlider[index].value = currentReloadTime / reloadTime;
    }

    public void UpdateWeaponImage(Sprite weapon1Sprite, Sprite weapon2Sprite)
    {
        weaponImages[0].sprite = weapon1Sprite;
        weaponImages[1].sprite = weapon2Sprite;
    }

    public void SwitchWeapon()
    {
        weaponUI[WeaponManager.Instance.CurrentWeaponIndex].transform.SetAsLastSibling();
    }
}