using System.Collections;
using System.Collections.Generic;
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

    // Weapon 
    private void Start()
    {
        currentWeaponIndex = 0;
        
        //임시 총 초기화
        equipedWeapons[0] = playerObject.AddComponent<Pistol>();
        equipedWeapons[1] = playerObject.AddComponent<Rifle>();

        equipedWeapons[0].Initialized(WeaponType.Pistol);
        equipedWeapons[1].Initialized(WeaponType.Rifle);
    }

    // Fire Bullet
    public bool CanAttack(){
        if(!currentWeapon.CanFire()){
            Debug.Log("재장전 중입니다.");
            return false;
        }
        return true;
    }

    public void Attack()
    {
        Transform bulletPos = playerObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips);
        Quaternion bulletRot = playerObject.transform.rotation;

        currentWeapon.Fire(bulletObject, bulletPos, bulletRot);
    }

    // Change Weapon
    public void SwitchWeapon(){
        currentWeaponIndex = 1 - currentWeaponIndex;
        Debug.Log("무기 교체 : " + equipedWeapons[currentWeaponIndex].name);
        //무기 교체 추가 작업칸
    }
}