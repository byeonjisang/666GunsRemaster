using System;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle,
    Shotgun,
    Sniper,
}

public class WeaponManager : Singleton<WeaponManager>
{
    [Header("Camera Component")]
    [SerializeField]
    private CameraController cameraControl;

    [Header("Weapon Component")]
    [SerializeField]
    private GameObject bulletObject;
    [SerializeField]
    private GameObject playerObject;

    private int currentWeaponIndex = 0;
    public int CurrentWeaponIndex => currentWeaponIndex;
    private Weapon[] equipedWeapons = new Weapon[2];
    private Weapon currentWeapon => equipedWeapons[currentWeaponIndex];
    private List<Weapon> WeaponTestWeapons = new List<Weapon>();

    // 무기 교체 쿨타임 관련 변수
    [Header("Weapon Change Settings")]
    [SerializeField]
    private float changeCooldown = 5.0f;
    private float currentChangeTime = 0.0f;

    public bool IsChangeCooldownActive => currentChangeTime > 0;

    // Weapon Initialization in WeaopnTestRoom
    public void Initialized(int weaponIndex)
    {
        currentWeaponIndex = 0;
        if (equipedWeapons[0] != null)
        {
            WeaponTestWeapons.Add(equipedWeapons[0]);
            equipedWeapons[0].gameObject.SetActive(false);
            equipedWeapons[currentWeaponIndex].SetWeaponRig(false);
        }


        bool isWeaponFound = false;
        foreach (Weapon weapon in WeaponTestWeapons)
        {
            if (weapon.Type == (WeaponType)weaponIndex)
            {
                equipedWeapons[0] = weapon;
                equipedWeapons[0].gameObject.SetActive(true);
                equipedWeapons[0].SetWeaponRig();
                isWeaponFound = true;
                break;
            }
        }
        if (!isWeaponFound)
        {
            WeaponLoader weaponLoader = GameObject.FindObjectOfType<WeaponLoader>();
            if (weaponLoader == null)
            {
                Debug.LogError("WeaponLoader not found in the scene.");
                return;
            }
            equipedWeapons[0] = weaponLoader.LoadWeapon(0, (WeaponType)weaponIndex);    
        }

        WeaponUIEvents.OnUpdateWeaponImage?.Invoke(equipedWeapons[0].GetWeaponSprite(), null);
        int[] bullet = currentWeapon.GetBullet();
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, bullet[0], bullet[1]);
    }

    // Weapon Initialization in Stage Map
    public void Initialized(int weapon1Index = 0, int weapon2Index = 1)
    {
        currentWeaponIndex = 0;

        WeaponLoader weaponLoader = GameObject.FindObjectOfType<WeaponLoader>();
        if (weaponLoader == null)
        {
            Debug.LogError("WeaponLoader not found in the scene.");
            return;
        }

        // 무기 초기화
        equipedWeapons[0] = weaponLoader.LoadWeapon(0, (WeaponType)weapon1Index);
        equipedWeapons[1] = weaponLoader.LoadWeapon(1, (WeaponType)weapon2Index);

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
        currentWeapon.Fire(bulletObject);
        int[] bulletCount = currentWeapon.GetBullet();
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, bulletCount[0], bulletCount[1]);

        // 카메라 흔들기
        cameraControl.ShakeCamera(0.2f, 0.2f, 0.2f);
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

        equipedWeapons[currentWeaponIndex].SetWeaponRig(false);
        equipedWeapons[currentWeaponIndex].gameObject.SetActive(false);

        currentWeaponIndex = 1 - currentWeaponIndex;

        equipedWeapons[currentWeaponIndex].gameObject.SetActive(true);
        equipedWeapons[currentWeaponIndex].SetWeaponRig();        

        currentChangeTime = changeCooldown;
        WeaponUIEvents.OnSwitchWeaponUI?.Invoke();
        WeaponUIEvents.OnUpdateCooldownUI?.Invoke(changeCooldown);
        WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, currentWeapon.GetBullet()[0], currentWeapon.GetBullet()[1]);
        Debug.Log("무기 교체 : " + equipedWeapons[currentWeaponIndex].name);
        //무기 교체 추가 작업칸
    }

    /// <summary>
    /// 특정 슬롯에 무기를 교체하는 함수
    /// </summary>
    /// <param name="slotIndex">0 또는 1</param>
    /// <param name="weaponType">장착할 무기 타입</param>
    public void ReplaceWeapon(int slotIndex, WeaponType weaponType)
    {
        //예외처리
        if (slotIndex < 0 || slotIndex >= equipedWeapons.Length)
        {
            Debug.LogWarning("무기 슬롯 인덱스가 잘못됨");
            return;
        }

        // 기존 무기 제거
        if (equipedWeapons[slotIndex] != null)
        {
            Destroy(equipedWeapons[slotIndex]);
        }

        Weapon newWeapon = null;

        // 무기 타입에 따라 AddComponent
        switch (weaponType)
        {
            case WeaponType.Pistol:
                newWeapon = playerObject.AddComponent<Pistol>();
                break;
            case WeaponType.Rifle:
                newWeapon = playerObject.AddComponent<Rifle>();
                break;
            case WeaponType.Shotgun:
                newWeapon = playerObject.AddComponent<Shotgun>();
                break;
            default:
                Debug.LogWarning("해당 무기 타입이 없습니다.");
                return;
        }

        newWeapon.Initialized(slotIndex, weaponType);
        equipedWeapons[slotIndex] = newWeapon;

        // UI 업데이트
        WeaponUIEvents.OnUpdateWeaponImage?.Invoke(equipedWeapons[0].GetWeaponSprite(), equipedWeapons[1].GetWeaponSprite());
        if (slotIndex == currentWeaponIndex)
        {
            int[] bullet = newWeapon.GetBullet();
            WeaponUIEvents.OnUpdateBulletUI?.Invoke(currentWeaponIndex, bullet[0], bullet[1]);
        }

        Debug.Log($"[WeaponManager] {slotIndex}번 슬롯 무기 교체 완료: {weaponType}");
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