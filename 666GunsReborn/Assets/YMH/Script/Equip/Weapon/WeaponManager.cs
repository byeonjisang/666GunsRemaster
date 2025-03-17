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
    [Header("Weapon Component")]
    [SerializeField]
    private GameObject bulletObject;
    [SerializeField]
    private GameObject playerObject;

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

        currentWeapon.Init(weaponType);
    }

    public void Attack()
    {
        Transform bulletPos = playerObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips);
        Quaternion bulletRot = playerObject.transform.rotation;

        currentWeapon.Fire(bulletObject, bulletPos, bulletRot);
    }
}