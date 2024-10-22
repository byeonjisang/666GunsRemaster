using Character.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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

        private Gun currentGun;   //현재 사용중인 총
        [SerializeField]// 임시 총 부여
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private GameObject[] guns;           //모든 총들
        private int currentGunIndex = 0;     //현재 착용한 총의 인덱스
        private int theNumberOfGun = 2;      //총의 개수

        //무기 발사 관련
        private bool isFiring = false;
        //무기 줍기 기능 관련
        private string keepGunName;

        [SerializeField]
        private Button WeaponChangeButton;
        [SerializeField]
        private Button WeaponGetButton;
        [SerializeField]
        private Button WeaponDeleteButton;

        private void Start()
        {
            //총 전체 비활성화
            foreach(GameObject gun in guns)
            {
                gun.SetActive(false);
            }

            //착용 중인 총 활성화
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //무기 변경 버튼 이벤트 추가
            WeaponChangeButton.onClick.AddListener(ChangeGun);

            WeaponGetButton.onClick.AddListener(() => ChangePossessionGuns(keepGunName));
            WeaponDeleteButton.onClick.AddListener(DeleteGun);
        }

        public void OnPointerDown()
        {
            if (!isFiring)
            {
                StartCoroutine(FireContinuously());
            }
        }
        public void OnPointerUp()
        {
            isFiring = false;
        }
        private IEnumerator FireContinuously()
        {
            isFiring = true;
            float fireRate = currentGun.FireRate;
            while (isFiring)
            {
                if (PlayerController.Instance.IsOverHit)
                {
                    yield return null;
                }
                else if (currentGun != null)
                {
                    currentGun.Fire();
                    PlayerController.Instance.OverHit();
                }
                yield return new WaitForSeconds(fireRate);
            }
        }

        //무기 변경
        private void ChangeGun()
        {
            if (theNumberOfGun < 2)
                return;

            currentGunIndex = 1 - currentGunIndex;

            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();
            possessionGuns[1 - currentGunIndex].SetActive(false);

            //임팩트 효과

            //사운드
        }
        //무기 교체
        public void ChangePossessionGuns(string gunName)
        {
            if (gunName == null)
                return;

            GameObject gunObject = guns.FirstOrDefault(gun => gun.name == gunName);
            possessionGuns[currentGunIndex].SetActive(false);
            possessionGuns[currentGunIndex] = gunObject;
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            if (gunName != "Pistol")
                keepGunName = null;
        }
        //무기 보유
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName;
        }

        //무기 삭제
        private void DeleteGun()
        {
            if (keepGunName == null)
                return;

            keepGunName = null;
        }

        //데미지 반환
        public float GetDamage()
        {
            return currentGun.Damage;
        }
    }
}