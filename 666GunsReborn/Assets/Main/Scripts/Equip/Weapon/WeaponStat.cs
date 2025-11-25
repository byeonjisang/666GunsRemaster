using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Weapon
{
    public class WeaponStat
    {
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

        // 무기 스탯 초기화
        public WeaponStat(int index, WeaponData weaponData)
        {
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
        }

        // 무기 스텟 업데이트
        public void WeaponStatUpdate()
        {
            // 재장전 시간 업데이트
            if (_currentReloadTime > 0)
            {
                _currentReloadTime -= Time.deltaTime;
                WeaponUIEvents.OnUpdateReloadSlider?.Invoke(_index, _reloadTime, _currentReloadTime);
                if (_currentReloadTime <= 0)
                {
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

        /// <summary>
        /// 무기 발사
        /// </summary>
        public void Fire()
        {
            _currentMagazine--;
            _fireCooldown = 1.0f / _fireSpeed;
            if (_currentMagazine <= 0)
            {
                _currentReloadTime = _reloadTime;
            }
        }

        // 재장전
        private void Reload()
        {
            _currentMagazine = _maxMagazine;
            WeaponUIEvents.OnUpdateBulletUI?.Invoke(_index, _maxMagazine, _currentMagazine);
        }
    }   
}