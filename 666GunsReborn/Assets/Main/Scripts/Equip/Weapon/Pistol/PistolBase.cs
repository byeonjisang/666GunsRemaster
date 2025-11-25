using UnityEngine;
using UnityEngine.Pool;

namespace Weapon
{
    public class PistolBase : WeaponBase
    {
        // 권총 발사 메서드
        protected override void TryFire()
        {
            base.TryFire();
            if(!IsReadyToFire())
                return;

            GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Pistol", _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Initialization(WeaponStat.Power, WeaponStat.BulletSpeed);
            WeaponStat.Fire();

            //애니메이션
            //사운드
            PlayFireSound();
        }
    }
}