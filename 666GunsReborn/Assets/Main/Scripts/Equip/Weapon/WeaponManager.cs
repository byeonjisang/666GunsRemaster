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
    public int CurrentWeaponIndex => currentWeaponIndex;
    private Weapon[] equipedWeapons = new Weapon[2];
    private Weapon currentWeapon => equipedWeapons[currentWeaponIndex];

    // 무기 교체 쿨타임 관련 변수
    [Header("Weapon Change Settings")]
    [SerializeField]
    private float changeCooldown = 5.0f;
    private float currentChangeTime = 0.0f;

    public bool IsChangeCooldownActive => currentChangeTime > 0;

    // Weapon test initialization
    private void Start()
    {
        currentWeaponIndex = 0;

        //임시 총 초기화
        equipedWeapons[0] = playerObject.AddComponent<Pistol>();
        equipedWeapons[1] = playerObject.AddComponent<Rifle>();

        equipedWeapons[0].Initialized(0, WeaponType.Pistol);
        equipedWeapons[1].Initialized(1, WeaponType.Rifle);

        //UI 초기화
        WeaponUIEvents.OnUpdateWeaponImage?.Invoke(equipedWeapons[0].GetWeaponSprite(), equipedWeapons[1].GetWeaponSprite());
        int[] bullet = currentWeapon.GetBullet();
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, bullet[0], bullet[1]);
    }

    // Check if can attack
    public bool CanAttack()
    {
        if (!currentWeapon.CanFire())
        {
            Debug.Log("재장전 중입니다.");
            return false;
        }
        return true;
    }

    // Weapon Attack
    public void Attack()
    {
        Transform bulletPos = playerObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/Weapon Point/BulletSpawnPoint");

        Quaternion bulletRot = playerObject.transform.rotation;

        currentWeapon.Fire(bulletObject, bulletPos, bulletRot);
        int[] bullet = currentWeapon.GetBullet();
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, bullet[0], bullet[1]);
    }

    // Change Weapon
    public void SwitchWeapon()
    {
        Debug.Log("무기 교체 시도");
        if (IsChangeCooldownActive)
        {
            Debug.Log("무기 교체 쿨타임 중입니다.");
            return;
        }

        currentWeaponIndex = 1 - currentWeaponIndex;
        currentChangeTime = changeCooldown;
        WeaponUIEvents.OnSwitchWeaponUI?.Invoke();
        WeaponUIEvents.OnUpdateCooldownUI?.Invoke(changeCooldown);
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, currentWeapon.GetBullet()[0], currentWeapon.GetBullet()[1]);
        Debug.Log("무기 교체 : " + equipedWeapons[currentWeaponIndex].name);
        //무기 교체 추가 작업칸
    }

    private void Update()
    {
        if (currentChangeTime > 0)
        {
            currentChangeTime -= Time.deltaTime;
            WeaponUIEvents.OnUpdateCooldownUI?.Invoke(currentChangeTime);
        }
    }
}