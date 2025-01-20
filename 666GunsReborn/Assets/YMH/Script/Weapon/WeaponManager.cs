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
    private WeaponType weaponType;
    private Weapon currentWeapon;

    public void Attack()
    {
        currentWeapon.Fire();
    }
}