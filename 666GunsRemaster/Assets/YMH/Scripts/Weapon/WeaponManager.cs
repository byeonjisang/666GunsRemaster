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

        private Gun currentGun;   //���� ������� ��
        [SerializeField]// �ӽ� �� �ο�
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private GameObject[] guns;           //��� �ѵ�
        private int currentGunIndex = 0;     //���� ������ ���� �ε���
        private int theNumberOfGun = 2;      //���� ����

<<<<<<< HEAD
=======
        //���� �߻� ����
        private bool isFiring = false;

>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
        //���� �ݱ� ��� ����
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
            //���� ���� �� Ȱ��ȭ
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //���� ���� ��ư �̺�Ʈ �߰�
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

        //���� ����
        private void ChangeGun()
        {
            if (theNumberOfGun < 2)
                return;

            currentGunIndex = 1 - currentGunIndex;

            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();
            possessionGuns[1 - currentGunIndex].SetActive(false);

            //����Ʈ ȿ��

            //����
        }
<<<<<<< HEAD
        //���� �ݱ�
=======
        //���� ��ü
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
        //���� ����
>>>>>>> 70c8e2f31a85e687722d7301baee4fab9eecbe10
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName;
        }
<<<<<<< HEAD
        //���� ȹ��
        private void GetGun()
=======
        //���� ����
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
        //�⺻ ����� ����
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

        //������ ��ȯ
        public float GetDamage()
        {
            return currentGun.Damage;
        }
    }
}