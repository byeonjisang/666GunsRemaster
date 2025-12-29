using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    [Header("Weapon Button UI")]
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public void Init(WeaponData weaponData)
    {
        _image.sprite = weaponData.weaponSprite;
        _image.SetNativeSize();
    }
}