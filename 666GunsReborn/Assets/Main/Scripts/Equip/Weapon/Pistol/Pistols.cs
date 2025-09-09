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

        #region Get Info
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public WeaponType GetWeaponType()
        {
            return weaponStats.WeaponType;
        }
        #endregion


        #region Weapon Fire
        public void Fire()
        {
            if (!IsReadyToFire())
                return;

            GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Pistol", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Initialization(weaponStats.Power, weaponStats.BulletSpeed);
            weaponStats.Fire();

            //애니메이션
            //사운드
            PlayWeaponSound();
        }

        public void StopFire()
        {
            
        }

        private bool IsReadyToFire()
        {
            if (!weaponStats.IsReadyToFire())
            {
                Debug.Log("Weapon is not ready to fire.");
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