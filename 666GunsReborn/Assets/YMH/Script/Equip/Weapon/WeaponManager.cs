using System;
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

    private int currentWeaponIndex = 0;
    private Weapon[] equipedWeapons = new Weapon[2];
    private Weapon currentWeapon => equipedWeapons[currentWeaponIndex];
    
    
    public Action<int, int> OnUpdateAmmoUI;
    public Action<Sprite> OnUpdateWeaponImage;

    // Weapon test initialization
    private void Start()
    {
        currentWeaponIndex = 0;

        //임시 총 초기화
        equipedWeapons[0] = playerObject.AddComponent<Pistol>();
        equipedWeapons[1] = playerObject.AddComponent<Rifle>();

        equipedWeapons[0].Initialized(WeaponType.Pistol);
        equipedWeapons[1].Initialized(WeaponType.Rifle);

        //UI 초기화
        OnUpdateWeaponImage?.Invoke(currentWeapon.GetWeaponSprite());
        int[] ammo = currentWeapon.GetAmmo();
        OnUpdateAmmoUI?.Invoke(ammo[0], ammo[1]);
    }

    // Check if can attack
    public bool CanAttack(){
        if(!currentWeapon.CanFire()){
            Debug.Log("재장전 중입니다.");
            return false;
        }
        return true;
    }

    // Weapon Attack
    public void Attack()
    {
        Transform bulletPos = playerObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips);
        Quaternion bulletRot = playerObject.transform.rotation;

        currentWeapon.Fire(bulletObject, bulletPos, bulletRot);
        int[] ammo = currentWeapon.GetAmmo();
        OnUpdateAmmoUI?.Invoke(ammo[0], ammo[1]);
    }

    // Change Weapon
    public void SwitchWeapon(){
        currentWeaponIndex = 1 - currentWeaponIndex;
        OnUpdateWeaponImage?.Invoke(currentWeapon.GetWeaponSprite());
        OnUpdateAmmoUI?.Invoke(currentWeapon.GetAmmo()[0], currentWeapon.GetAmmo()[1]);
        Debug.Log("무기 교체 : " + equipedWeapons[currentWeaponIndex].name);
        //무기 교체 추가 작업칸
    }
}