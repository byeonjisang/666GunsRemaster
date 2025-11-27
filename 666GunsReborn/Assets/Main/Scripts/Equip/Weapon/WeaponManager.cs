using UnityEngine;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace Weapon
{
    public class WeaponManager
    {
        private PlayerChannel _playerChannel;

        // 현재 소지 중인 무기들
        private IWeapon[] equipedWeapons = new IWeapon[2];
        // 현재 장착 중인 무기 인덱스
        // 0 : 무기 1, 1 : 무기 2, null : 무기 없음
        private int? currentWeaponIndex = 0;
        // 현재 장착 중인 무기 외부 참조
        private IWeapon currentWeapon => equipedWeapons[currentWeaponIndex.Value];

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
        public void Init(WeaponID weapon1ID, WeaponID weapon2ID)
        {
            Debug.Log("WeaponManager Initialization");
            currentWeaponIndex = 0;
            
            // TODOl: 다 수정 필요, 너무 하드코딩 느낌이 있음
            WeaponLoader weaponLoader = GameObject.FindObjectOfType<WeaponLoader>();
            if (weaponLoader == null)
            {
                Debug.LogError("WeaponLoader not found in the scene.");
                return;
            }

            // 무기 불러오기
            weaponLoader.LoadWeapon(0, weapon1ID, (loadedWeapon) =>
            {
                equipedWeapons[0] = loadedWeapon;   
            });
            weaponLoader.LoadWeapon(1, weapon2ID, (loadedWeapon) =>
            {
                equipedWeapons[1] = loadedWeapon;
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
            equipedWeapons[currentWeaponIndex.Value].GameObject.SetActive(false);
            currentWeaponIndex = 1 - currentWeaponIndex.Value;
            equipedWeapons[currentWeaponIndex.Value].GameObject.SetActive(true);

            // ui 업데이트 이벤트 발생
            _playerChannel.SendWeaponChanged(currentWeaponIndex.Value);

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

