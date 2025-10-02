using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public class Shotgun : MonoBehaviour, IWeapon
    {
        private WeaponStats weaponStats;

        [SerializeField]
        private Transform bulletSpawnPoint;

        private int bulletCount = 3; // 발사할 때 한 번에 나가는 총알 수

        #region Weapon Initialization
        public void Initialization(int index, WeaponID weaponName)
        {
            weaponStats = gameObject.AddComponent<WeaponStats>();
            string path = $"Datas/Weapon/Shotgun/{weaponName.ToString()}";
            WeaponData weaponData = Resources.Load<WeaponData>(path);
            if (weaponData == null)
            {
                Debug.LogError($"Weapon data not found at path : {path}");
                return;
            }
            weaponStats.Initialized(index, weaponData);
        }
        #endregion

        #region Get GameObject
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

            for (int i = 0; i < bulletCount; i++)
            {
                //bulletSpawnPoint.rotation = Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
                GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Shotgun", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.transform.Rotate(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
                bullet.GetComponent<Bullet>().Initialization(weaponStats.Power, weaponStats.BulletSpeed);
                weaponStats.Fire();

                //애니메이션
                //사운드
                PlayWeaponSound();
            }
        }

        public void StopFire()
        {
            
        }

        private bool IsReadyToFire()
        {
            if (!weaponStats.IsReadyToFire())
            {
                Debug.Log("Weapon is reloading.");
                return false;
            }
            return true;
        }

        protected virtual void PlayWeaponSound()
        {
            Debug.Log("Play weapon sound");
            //SoundManagers.Instance.PlayOneShot(SFX.Shotgun_Fire, transform.position);
        }
        #endregion
    }    
}