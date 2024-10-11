using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gun
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private Gun currentGun;   //현재 사용중인 총알
        [SerializeField]// 임시 총 부여
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private Button WeaponChangeButton;

        private int currentWeaponIndex = 0;     //현재 착용한 총의 인덱스

        private void Start()
        {
            //착용하지 않는 총 비활성화
            possessionGuns[1 - currentWeaponIndex].SetActive(false);
            currentGun = possessionGuns[currentWeaponIndex].GetComponent<Gun>();

            //무기 변경 버튼 이벤트 추가
            WeaponChangeButton.onClick.AddListener(ChangeWeapon);
        }

        //무기 변경
        private void ChangeWeapon()
        {
            currentWeaponIndex = 1 - currentWeaponIndex;

            possessionGuns[currentWeaponIndex].SetActive(true);
            currentGun = possessionGuns[currentWeaponIndex].GetComponent<Gun>();
            possessionGuns[1 - currentWeaponIndex].SetActive(false);

            //임팩트 효과
        }

        //데미지 반환
        public float GetDamage()
        {
            return currentGun.Damage;
        }
    }
}