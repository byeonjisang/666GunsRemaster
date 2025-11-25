using Unity.Mathematics;
using UnityEngine;

namespace Weapon
{
    public class WeaponBase : MonoBehaviour, IWeapon
    {
        // 무기 데이터
        [Header("Weapon Data")]
        [SerializeField] protected WeaponData weaponData;
        // 무기 스텟
        public WeaponStat WeaponStat { get; private set; }

        [Header("Weapon Spawn Point")]
        // 총알 생성 위치
        [SerializeField]
        protected Transform _bulletSpawnPoint;
        // 트리거가 눌러져 있는지 여부
        protected bool _isTriggerHeld = false;

        // 총 GameObject 참조
        public GameObject GameObject => this.gameObject;
        // 무기 발사 여부
        public bool IsFiring => _isTriggerHeld;

        #region Weapon Init
        /// <summary>
        /// 무기 초기화
        /// </summary>
        /// <param name="index"></param>
        /// <param name="weaponName"></param>
        public virtual void Init(int index, WeaponID weaponName)
        {
            // 무기 데이터 로드
            // TODO: Resources.Load 성능이 않좋아서 Addressables로 변경 필요
            WeaponStat = new WeaponStat(index, weaponData);
        }
        #endregion

        #region Weapon Start Fire and Stop Fire
        /// <summary>
        /// 발사 시작
        /// </summary>
        public void StartFire()
        {
            _isTriggerHeld = true;
        }

        /// <summary>
        /// 발사 중지
        /// </summary>
        public void StopFire()
        {
            _isTriggerHeld = false;
        }

        // 트리거를 누르면 매 프레임마다 발사 시도
        protected virtual void Update()
        {
            if (_isTriggerHeld)
            {
                TryFire();
            }

            // 무기 스탯 업데이트
            WeaponStat.WeaponStatUpdate();
        }

        // 총알 발사 시도
        protected virtual void TryFire()
        {
            // 총기마다 따로 구현
        }

        // 발사 가능 여부 체크
        protected bool IsReadyToFire()
        {
            if (!WeaponStat.IsReadyToFire())
            {
                Debug.Log("Weapon is not ready to fire.");
                return false;
            }
            return true;
        }
        #endregion

        #region Play Weapon Sound
        // 무기 발사 사운드 재생
        protected virtual void PlayFireSound()
        {
            // 무기 마다 사운드 따로 구현
        }
        #endregion
    }
}