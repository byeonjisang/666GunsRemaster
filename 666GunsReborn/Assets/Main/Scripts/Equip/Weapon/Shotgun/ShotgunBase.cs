using Unity.VisualScripting;
using UnityEngine;

namespace Weapon
{
    public class ShotgunBase : WeaponBase
    {
        // #region Weapon Fire
        // public void Fire()
        // {
        //     if (!IsReadyToFire())
        //         return;

        //     for (int i = 0; i < bulletCount; i++)
        //     {
        //         //bulletSpawnPoint.rotation = Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
        //         GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Shotgun", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        //         bullet.transform.Rotate(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
        //         bullet.GetComponent<Bullet>().Initialization(weaponStat.Power, weaponStat.BulletSpeed);
        //         weaponStat.Fire();

        //         //애니메이션
        //         //사운드
        //         PlayWeaponSound();
        //     }
        // }
    }    
}