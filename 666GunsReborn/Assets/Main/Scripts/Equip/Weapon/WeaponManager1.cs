using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons
{
    public class WeaponManager1 : Singleton<WeaponManager1>
    {
        // 이벤트 리스너
        public UnityEvent SwitchWeaponEvent;

        // 무기 관련 변수들
        private IWeapon[] equipedWeapons = new IWeapon[2];
        private int currentWeaponIndex = 0;
        private IWeapon currentWeapon => equipedWeapons[currentWeaponIndex];

        // 무기 교체 쿨타임 관련 변수
        private float changeCooldown = 5.0f;
        private float currentChangeTime = 0.0f;

        private bool isFiring = false;

        public void Initialization(int weapon1Index, int weapon2Index)
        {
            Debug.Log("WeaponManager Initialization");
            currentWeaponIndex = 0;

            WeaponLoader weaponLoader = GameObject.FindObjectOfType<WeaponLoader>();
            if (weaponLoader == null)
            {
                Debug.LogError("WeaponLoader not found in the scene.");
                return;
            }

            // 무기 불러오기
            equipedWeapons[0] = weaponLoader.LoadWeapon(0, (WeaponID)weapon1Index);
            equipedWeapons[1] = weaponLoader.LoadWeapon(1, (WeaponID)weapon2Index);

            equipedWeapons[0].GetGameObject().SetActive(true);
            SwitchWeaponEvent?.Invoke();
        }

        public void OnFire()
        {
            //currentWeapon.Fire();
            isFiring = true;
        }

        public void OffFire()
        {
            isFiring = false;
            currentWeapon.StopFire();
        }

        public void SwitchWeapon()
        {
            if (currentChangeTime > 0)
            {
                Debug.Log("무기 교체 쿨타임");
                return;
            }

            // 무기 교체
            equipedWeapons[currentWeaponIndex].GetGameObject().SetActive(false);
            currentWeaponIndex = 1 - currentWeaponIndex;
            equipedWeapons[currentWeaponIndex].GetGameObject().SetActive(true);

            // 교체 쿨타임 적용
            currentChangeTime = changeCooldown;

            SwitchWeaponEvent?.Invoke();
        }

        public WeaponType GetCurrentWeaponType()
        {
            return currentWeapon.GetWeaponType();
        }

        private void Update()
        {
            if (currentChangeTime > 0)
            {
                currentChangeTime -= Time.deltaTime;
            }

            if (isFiring)
            {
                currentWeapon.Fire();
            }
        }
    }    
}

