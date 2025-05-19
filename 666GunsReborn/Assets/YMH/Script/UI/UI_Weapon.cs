using UnityEngine;
using UnityEngine.UI;

public class UI_Weapon : MonoBehaviour
{
    [SerializeField]
    private Image weaponImage;
    [SerializeField]
    private Text weaponAmmoText;

    [SerializeField]
    private Slider reloadCooldownSlider;

    private void Awake()
    {
        WeaponManager.Instance.OnUpdateAmmoUI += UpdateAmmoUI;
        WeaponManager.Instance.OnUpdateWeaponImage += UpdateWeaponImage;
        WeaponManager.Instance.OnUpdateReLoadCooldownUI += UpdateReloadCooldownUI;
    }

    private void UpdateAmmoUI(int currentAmmo, int currentMagazine)
    {
        weaponAmmoText.text = currentMagazine + " / " + currentAmmo;
    }

    private void UpdateWeaponImage(Sprite weaponSprite)
    {
        weaponImage.sprite = weaponSprite;
    }

    private void UpdateReloadCooldownUI(float cooldown)
    {
        reloadCooldownSlider.value = cooldown;
    }
}