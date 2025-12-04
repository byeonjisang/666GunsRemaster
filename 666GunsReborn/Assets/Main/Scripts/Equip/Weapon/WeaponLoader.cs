using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System;

namespace Weapon
{
    public class WeaponLoader : MonoBehaviour
    {
        // 무기 프리팹 주소 참조 리스트
        [SerializeField]
        private List<AssetReferenceGameObject> weaponPrefabs;

        // 무기 프리팹을 소환해야할 위치(플레이어 손)
        [SerializeField]
        private Transform weaponParent;
        
        // Addressables 초기화
        private void Start()
        {
            StartCoroutine(InitAddressable());
        }
        // Addressables 초기화 코루틴
        private IEnumerator InitAddressable()
        {
            var init = Addressables.InitializeAsync();
            yield return init;
        }

        /// <summary>
        /// 무기 프리팹을 생성하는 메서드
        /// </summary>
        /// <param name="index"></param>
        /// <param name="weaponID"></param>
        public void LoadWeapon(int index, WeaponID weaponID, Action<IWeapon> onWeaponLoaded)
        {
            // Addressables를 통해 무기 프리팹 로드 및 인스턴스화
            weaponPrefabs[(int)weaponID].InstantiateAsync(weaponParent).Completed += (obj) =>
            {
                // 인스턴스화된 무기 프리팹 가져오기
                GameObject weaponInstance = obj.Result;

                // 무기 컴포넌트 초기화
                IWeapon weaponComponent = weaponInstance.GetComponent<IWeapon>();
                weaponComponent.Init(index);

                // 무기 비활성화(장착 중인 무기만 활성화)
                weaponInstance.SetActive(false);
                if(index == 0)
                    weaponInstance.SetActive(true); 

                // 콜백 호출
                onWeaponLoaded?.Invoke(weaponComponent);
            };
        }
    }
}