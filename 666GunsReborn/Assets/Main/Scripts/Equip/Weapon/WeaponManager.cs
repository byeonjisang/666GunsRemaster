using UnityEngine;
using System.Threading.Tasks;
using Unity.Mathematics;
using System.Collections.Generic;

namespace Weapon
{
    public class WeaponManager
    {
        private PlayerChannel _playerChannel;

        // 현재 소지 중인 무기들
        private IWeapon[] _equipedWeapons = new IWeapon[2];
        // 현재 장착 중인 무기 인덱스
        // 0 : 무기 1, 1 : 무기 2, null : 무기 없음
        private int? _currentWeaponIndex = 0;
        // 현재 장착 중인 무기 외부 참조
        private IWeapon currentWeapon => _equipedWeapons[_currentWeaponIndex.Value];

        // 무기 교체 쿨타임
        private float changeCooldown = 5.0f;
        // 현재 무기 교체 쿨타임 타이머
        private float currentChangeTime = 0.0f;

        // 현재 무기 발사 중인지 여부
        public bool IsFiring
        {
            get
            {
                if (currentWeapon == null)
                    return false;
                return currentWeapon.IsFiring;
            }
        }

        public WeaponManager(PlayerChannel playerChannel)
        {
            _playerChannel = playerChannel;
        }

        // WeaponManager 초기화
        public void Init(WeaponID[] selectWeaponsID, Transform weaponSpawnPoint)
        {
            _currentWeaponIndex = 0;
            
            // 무기 로드 및 장착
            AddressablesLoader.LoadAssetByLabel<GameObject>("Weapon", (loadedWeapons) =>
            {
                foreach (var weapon in loadedWeapons)
                {
                    // 주무기 장착
                    if (weapon.name == selectWeaponsID[0].ToString())
                    {
                        GameObject instance = GameObject.Instantiate(weapon);
                        IWeapon iWeapon = instance.GetComponent<IWeapon>();
                        iWeapon.Init(0);
                        _equipedWeapons[0] = iWeapon;
                        _equipedWeapons[0].GameObject.SetActive(true);
                        _equipedWeapons[0].GameObject.transform.SetParent(weaponSpawnPoint, false);
                    }
                    // 보조무기 장착
                    else if (weapon.name == selectWeaponsID[1].ToString())
                    {
                        GameObject instance = GameObject.Instantiate(weapon);
                        IWeapon iWeapon = instance.GetComponent<IWeapon>();
                        iWeapon.Init(1);
                        _equipedWeapons[1] = iWeapon;
                        _equipedWeapons[1].GameObject.SetActive(false);
                        _equipedWeapons[1].GameObject.transform.SetParent(weaponSpawnPoint, false);
                    }
                }
            });
        }

        /// <summary>
        /// 무기 발사 시작
        /// </summary>
        public void OnFire()
        {
            if(currentWeapon != null)
                currentWeapon.StartFire();
        }

        /// <summary>
        /// 무기 발사 종료
        /// </summary>
        public void OffFire()
        {
            if (currentWeapon != null)
                currentWeapon.StopFire();
        }

        /// <summary>
        /// 무기 교체
        /// </summary>
        public void SwitchWeapon()
        {
            if (currentChangeTime > 0)
            {
                Debug.Log($"무기 교체 쿨타임 : {currentChangeTime:F2}초 남음");
                return;
            }

            // 무기 교체
            _equipedWeapons[_currentWeaponIndex.Value].GameObject.SetActive(false);
            _currentWeaponIndex = 1 - _currentWeaponIndex.Value;
            _equipedWeapons[_currentWeaponIndex.Value].GameObject.SetActive(true);

            // ui 업데이트 이벤트 발생
            _playerChannel.SendWeaponChanged(_currentWeaponIndex.Value);

            // 교체 쿨타임 적용
            currentChangeTime = changeCooldown;
        }

        /// <summary>
        /// WeaponManager 업데이트
        /// </summary>
        public void Update()
        {
            if (currentChangeTime > 0)
            {
                currentChangeTime -= Time.deltaTime;
                if (currentChangeTime < 0)
                    currentChangeTime = 0;
                
                // 교체 쿨타임 UI 업데이트 이벤트
                _playerChannel.SendChangedWeaponCooldown(currentChangeTime);
            }
        }
    }    
}

