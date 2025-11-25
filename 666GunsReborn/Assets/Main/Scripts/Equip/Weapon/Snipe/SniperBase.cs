using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Weapon
{
    public class SniperBase : WeaponBase
    {
        private SniperScopeConroller _sniperScopeConroller;
        [SerializeField] private GameObject scopeCameraObject;

        private bool _isAiming = false;

        #region Weapon Initialization
        // 초기화
        // public void Initialization(int index, WeaponID weaponName)
        // {
        //     // 무기 스탯 초기화
        //     weaponStat = new WeaponStat();
        //     string path = $"Datas/Weapon/Sniper/{weaponName.ToString()}";
        //     WeaponData weaponData = Resources.Load<WeaponData>(path);
        //     if (weaponData == null)
        //     {
        //         Debug.LogError($"Weapon data not found at path: {path}");
        //         return;
        //     }
        //     weaponStat.Initialized(index, weaponData);

        //     // 스코프 카메라 찾기
        //     //scopeCameraObject = gameObject.GetComponent<Camera>().gameObject;
        //     // 스코프 컨트롤러 찾기
        //     sniperScopeConroller = FindObjectOfType<SniperScopeConroller>();
        //     if (sniperScopeConroller != null)
        //         sniperScopeConroller.Aim(false, scopeCameraObject);
        // }
        #endregion

        /// <summary>
        /// 무기 총알 발사
        /// </summary>
        // public void Fire()
        // {
        //     if (!IsReadyToFire())
        //         return;

        //     if (!isAiming)
        //     {
        //         StartCoroutine(AimTransition());
        //     }
        //     else
        //     {
        //         GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Sniper", bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        //         bullet.GetComponent<Bullet>().Initialization(weaponStat.Power, weaponStat.BulletSpeed);
        //         weaponStat.Fire();

        //         // 애니메이션
        //         // 사운드
        //         PlayWeaponSound();
        //     }
        // }

        /// <summary>
        /// 무기 발사 중지
        /// </summary>
        // public void StopFire()
        // {
        //     isAiming = false;
        //     sniperScopeConroller.Aim(false, scopeCameraObject);
        //     // 스코프 UI 비활성화
        //     //scoreUI.SetActive(false);
        //     // 카메라에서 총 Layer 다시 포함
        //     // Camera.main.cullingMask |= ~(1 >> LayerMask.NameToLayer("Weapon"));

        //     // // 카메라 위치 복원
        //     // Debug.Log("Restoring camera position and rotation.");
        //     // Debug.Log($"Save Camera Position: {saveCameraPoint.position}, Rotation: {saveCameraPoint.rotation}");
        //     // Camera.main.transform.position = saveCameraPoint.position;
        //     // Camera.main.transform.rotation = saveCameraPoint.rotation;
        // }

        // 저격총 스코프 조준 시점으로 전환
        // private IEnumerator AimTransition()
        // {
        //     isAiming = true;
        //     sniperScopeConroller.Aim(true, scopeCameraObject);
        //     // 애니메이션 재생
        //     // 스코프 UI 활성화
        //     //scoreUI.SetActive(true);
        //     // 카메라에서 총 Layer 제외
        //     //Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Weapon"));

        //     // 원래 카메라 위치 저장
        //     // var cam = Camera.main;
        //     // saveCameraPoint = cam.transform;
        //     // Debug.Log($"Saving camera position: {saveCameraPoint.position}, rotation: {saveCameraPoint.rotation}");

        //     // // 카메라 줌 위치 변경
        //     // Camera.main.transform.position = scopePoint.position;
        //     // Camera.main.transform.rotation = scopePoint.rotation;

        //     yield return null;
        // }
    }    
}