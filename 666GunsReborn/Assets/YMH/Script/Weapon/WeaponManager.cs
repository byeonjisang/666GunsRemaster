using System.Collections;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun,
}
public enum BulletType 
{
    Normal,
    Fire,
    Ice,
    Poison,
}

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField]
    private GameObject bulletObject;
    [SerializeField]
    private Transform firePoint;

    private WeaponType weaponType;
    private Weapon currentWeapon;

    private void Start()
    {
        weaponType = WeaponType.Rifle;
        switch (weaponType)
        {
            case WeaponType.Pistol:
                currentWeapon = gameObject.AddComponent<Pistol>();
                break;
            case WeaponType.Rifle:
                currentWeapon = gameObject.AddComponent<Rifle>();
                break;
            case WeaponType.Shotgun:
                currentWeapon = gameObject.AddComponent<Shotgun>();
                break;
        }
    }

    public void Attack()
    {
        currentWeapon.Fire(bulletObject, firePoint);
    }
}