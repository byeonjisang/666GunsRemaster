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

<<<<<<< HEAD
=======
        //무기 발사 관련
        private bool isFiring = false;

>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
        //무기 줍기 기능 관련
        private string keepGunName;

        [SerializeField]
        private Button WeaponChangeButton;
<<<<<<< HEAD
        [SerializeField]
        private Button WeaponGetButton;
        [SerializeField]
        private Button GetLockButton;
=======
        private Image WeaponChangeButtonImage;
        private Text WeaponChangeButtonText;
        [SerializeField]
        private Button WeaponGetButton;
        [SerializeField]
        private Button WeaponDeleteButton;

>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10

        private void Start()
        {
            //착용 중인 총 활성화
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //무기 변경 버튼 이벤트 추가
            WeaponChangeButton.onClick.AddListener(ChangeGun);
<<<<<<< HEAD
            WeaponGetButton.onClick.AddListener(GetGun);
=======
            WeaponGetButton.onClick.AddListener(() => ChangePossessionGuns(keepGunName));
            WeaponDeleteButton.onClick.AddListener(DeleteGun);

            WeaponChangeButtonImage = WeaponChangeButton.GetComponentInChildren<Image>();
            WeaponChangeButtonText = WeaponChangeButton.GetComponentInChildren<Text>();
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
                if (currentGun != null)
                {
                    currentGun.Fire();
                }
                yield return new WaitForSeconds(fireRate);
            }
>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
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
<<<<<<< HEAD
        //무기 줍기
=======
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
>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName;
        }
<<<<<<< HEAD
        //무기 획득
        private void GetGun()
=======
        //무기 삭제
        private void DeleteGun()
>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
        {
            if (keepGunName == null)
                return;

<<<<<<< HEAD
            GameObject gunObject = guns.FirstOrDefault(gun => gun.name == keepGunName);
            possessionGuns[currentGunIndex].SetActive(false);
            possessionGuns[currentGunIndex] = gunObject;
            possessionGuns[currentGunIndex].SetActive(true);

            keepGunName = null;
        }
        //기본 무기로 변경
        public void ChangeDefaultGun()
        {
            GameObject pistolObject = guns.FirstOrDefault(gun => gun.name == "Pistol" && !gun.activeSelf);
            possessionGuns[currentGunIndex].SetActive(false);
            possessionGuns[currentGunIndex] = pistolObject;
            possessionGuns[currentGunIndex].SetActive(true);

=======
            keepGunName = null;
>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
        }

        //데미지 반환
        public float GetDamage()
        {
            return currentGun.Damage;
        }
    }
}