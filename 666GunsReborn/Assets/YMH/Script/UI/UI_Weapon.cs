using UnityEngine;
using UnityEngine.UI;

public class UI_Weapon : MonoBehaviour
{
    [SerializeField]
    private Image weaponImage;
    [SerializeField]
    private Text weaponAmmoText;

    private void Awake()
    {
        WeaponManager.Instance.OnUpdateAmmoUI += UpdateAmmoUI;
        WeaponManager.Instance.OnUpdateWeaponImage += UpdateWeaponImage;
    }

    public void UpdateAmmoUI(int currentAmmo, int currentMagazine)
    {
        weaponAmmoText.text = currentMagazine + " / " + currentAmmo;
    }

    public void UpdateWeaponImage(Sprite weaponSprite)
    {
        weaponImage.sprite = weaponSprite;
    }
}