using UnityEngine;
using UnityEngine.Pool;

namespace Weapons
{
    public class Pistols : MonoBehaviour, IWeapon
    {
        private WeaponStats weaponStats;

        [SerializeField]
        private Transform bulletSpawnPoint;

        #region Weapon Initialization

        public void Initialization(int index, WeaponID weaponName)
        {
            weaponStats = gameObject.AddComponent<WeaponStats>();
            string path = $"Datas/Weapon/Pistol/{weaponName.ToString()}";
            WeaponData weaponData = Resources.Load<WeaponData>(path);
            if (weaponData == null)
            {
                Debug.LogError($"Weapon data not found at path: {path}");
                return;
            }
            weaponStats.Initialized(index, weaponData);
        }
        #endregion

        #region Weapon Fire
        public void Fire()
        {
            if (!IsReadyToFire())
                return;

            GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Initialization(weaponStats.Power, weaponStats.BulletSpeed);
            weaponStats.Fire();

            //애니메이션
            //사운드
            PlayWeaponSound();
        }

        private bool IsReadyToFire()
        {
            if (weaponStats.IsReloading())
            {
                Debug.Log("Weapon is reloading.");
                return false;
            }
            return true;
        }

        protected virtual void PlayWeaponSound()
        {
            Debug.Log("Play weapon sound");
        }
        #endregion
    }
}