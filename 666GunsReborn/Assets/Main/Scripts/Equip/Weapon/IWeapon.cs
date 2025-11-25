using UnityEngine;

namespace Weapon
{
    public interface IWeapon
    {
        public GameObject GameObject { get; }
        // 무기 발사 여부
        public bool IsFiring { get; }

        // 무기 초기화
        public void Init(int index, WeaponID weaponName);
        // 총알 발사 시작
        public void StartFire();
        // 총알 발사 중지
        public void StopFire();
    }
}
   