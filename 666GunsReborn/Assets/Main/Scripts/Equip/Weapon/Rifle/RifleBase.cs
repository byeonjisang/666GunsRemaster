using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class RifleBase : WeaponBase
    {
        // 라이플 발사 메서드
        protected override void TryFire()
        {
            base.TryFire();
            if (!IsReadyToFire())
                return;

            GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Rifle", _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet.Bullet>().Initialization(WeaponStat.Power, WeaponStat.BulletSpeed);
            WeaponStat.Fire();

            // 애니메이션
            // 사운드
            PlayFireSound();
        }
    }
}