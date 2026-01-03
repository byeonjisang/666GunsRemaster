using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Weapon
{
    public class WeaponStat
    {
        #region Weapon Stat Variables
        private WeaponBase _weaponBase;

        // 무기 인덱스
        private int _index;

        // 무기 이미지
        private Sprite _weaponSprite;
        public Sprite WeaponSprite => _weaponSprite;
        // 무기 이름
        private string _weaponName;
        // 무기 공격력
        private float _power;
        public float Power => _power;
        // 발사 속도
        private float _fireSpeed;
        // 사격 쿨타임
        private float _fireCooldown;
        // 사격 거리
        private float _fireDistance;
        public float FireDistance => _fireDistance;
        // 무기 무게
        private float _weight;
        // 무기 종류
        private WeaponType _weaponType;
        public WeaponType WeaponType => _weaponType;
        // 현재 탄창
        private int _currentMagazine;
        public int CurrentMagazine => _currentMagazine;
        // 최대 탄창
        private int _maxMagazine;
        public int MaxMagazine => _maxMagazine;

        // 재장전 시간 측정용 변수
        public float _currentReloadTime;
        private float _reloadTime;
        // 총알 속도
        private float _bulletSpeed;
        public float BulletSpeed => _bulletSpeed;
        #endregion

        #region Weapon Stat Init
        // 무기 스탯 초기화
        public WeaponStat(WeaponBase weaponBase, int index, WeaponData weaponData)
        {
            _weaponBase = weaponBase;

            _index = index;
            _weaponSprite = weaponData.weaponSprite;
            _weaponName = weaponData.weaponName;
            _power = weaponData.power;
            _fireSpeed = weaponData.fireSpeed;
            _fireDistance = weaponData.fireDistance;
            _weight = weaponData.weight;
            _weaponType = weaponData.weaponType;
            _reloadTime = weaponData.reloadTime;
            _maxMagazine = weaponData.maxMagazine;
            _bulletSpeed = weaponData.bulletSpeed;

            _currentMagazine = _maxMagazine;
            _currentReloadTime = 0f;
            _fireCooldown = 0f;

            // 초기 총알 UI 업데이트
            // index를 반전 시켜야 정상 작동(왜 그러는지는 모름)
            _weaponBase.PlayerChannel.SendUpdateBullet(_maxMagazine, _currentMagazine, 1 - index);
            // 무기 이미지 초기화
            _weaponBase.PlayerChannel.SendWeaponSprite(index, _weaponSprite);
        }
        #endregion

        #region Weapon Stat Update
        // 무기 스텟 업데이트
        public void WeaponStatUpdate()
        {
            // 재장전 시간 업데이트
            if (_currentReloadTime > 0)
            {
                _currentReloadTime -= Time.deltaTime;
                _weaponBase.PlayerChannel.SendReloadTime(_reloadTime, _currentReloadTime);

                if (_currentReloadTime <= 0)
                {
                    _currentReloadTime = 0; 
                    _weaponBase.PlayerChannel.SendReloadTime(_reloadTime, _currentReloadTime);
                    Reload();
                }
            }

            // 사격 쿨타임 업데이트
            if (_fireCooldown > 0)
            {
                _fireCooldown -= Time.deltaTime;
                if (_fireCooldown < 0)
                {
                    _fireCooldown = 0;
                }
            }
        }
        #endregion

        #region Reload
        // 재장전
        private void Reload()
        {
            _currentMagazine = _maxMagazine;
            _weaponBase.PlayerChannel.SendUpdateBullet(_maxMagazine, _currentMagazine);
        }

        /// <summary>
        /// 재장전 중인지 여부
        /// </summary>
        /// <returns></returns>
        public bool IsReloading()
        {
            return _currentReloadTime > 0;
        }

        /// <summary>
        /// 발사 가능 여부
        /// </summary>
        /// <returns></returns>
        public bool IsReadyToFire()
        {
            return _fireCooldown <= 0 && _currentReloadTime <= 0;
        }
        #endregion

        #region Fire
        /// <summary>
        /// 무기 발사
        /// </summary>
        public void Fire()
        {
            _currentMagazine--;
            _weaponBase.PlayerChannel.SendUpdateBullet(_maxMagazine, _currentMagazine);
            _fireCooldown = 1.0f / _fireSpeed;
            if (_currentMagazine <= 0)
            {
                _currentReloadTime = _reloadTime;
            }
        }
        #endregion
    }   
}