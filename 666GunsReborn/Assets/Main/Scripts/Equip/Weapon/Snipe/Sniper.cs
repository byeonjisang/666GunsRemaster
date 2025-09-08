using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Weapons
{
    public class Sniper : MonoBehaviour, IWeapon
    {
        private WeaponStats weaponStats;

        [SerializeField]
        private Transform bulletSpawnPoint;

        [Header("Scope")]
        [SerializeField]
        private Transform scopePoint;
        [SerializeField]
        private GameObject scoreUI;

        private Transform saveCameraPoint;

        private bool isAiming = false;

        #region Weapon Initialization
        public void Initialization(int index, WeaponID weaponName)
        {
            weaponStats = gameObject.AddComponent<WeaponStats>();
            string path = $"Datas/Weapon/Sniper/{weaponName.ToString()}";
            WeaponData weaponData = Resources.Load<WeaponData>(path);
            if (weaponData == null)
            {
                Debug.LogError($"Weapon data not found at path: {path}");
                return;
            }
            weaponStats.Initialized(index, weaponData);

            // 스코프 UI 초기화
            scoreUI.SetActive(false);
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

            if (!isAiming)
            {
                StartCoroutine(AimThenFire());
            }
            else
            {
                GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Sniper", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Bullet>().Initialization(weaponStats.Power, weaponStats.BulletSpeed);
                weaponStats.Fire();

                // 애니메이션
                // 사운드
                PlayWeaponSound();
            }
        }

        public void StopFire()
        {
            isAiming = false;
            // 스코프 UI 비활성화
            scoreUI.SetActive(false);
            // 카메라에서 총 Layer 다시 포함
            Camera.main.cullingMask |= ~(1 >> LayerMask.NameToLayer("Weapon"));

            // 카메라 위치 복원
            Debug.Log("Restoring camera position and rotation.");
            Debug.Log($"Save Camera Position: {saveCameraPoint.position}, Rotation: {saveCameraPoint.rotation}");
            Camera.main.transform.position = saveCameraPoint.position;
            Camera.main.transform.rotation = saveCameraPoint.rotation;
        }

        private IEnumerator AimThenFire()
        {
            isAiming = true;
            // 애니메이션 재생
            // 스코프 UI 활성화
            scoreUI.SetActive(true);
            // 카메라에서 총 Layer 제외
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Weapon"));

            // 원래 카메라 위치 저장
            var cam = Camera.main;
            saveCameraPoint = cam.transform;
            Debug.Log($"Saving camera position: {saveCameraPoint.position}, rotation: {saveCameraPoint.rotation}");

            // 카메라 줌 위치 변경
            Camera.main.transform.position = scopePoint.position;
            Camera.main.transform.rotation = scopePoint.rotation;

            yield return null;
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
            SoundManagers.Instance.PlayOneShot(SFX.Sniper_Fire, transform.position);
        }
        #endregion
    }    
}