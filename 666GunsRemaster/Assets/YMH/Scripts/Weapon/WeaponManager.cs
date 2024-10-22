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

        private Gun currentGun;   //���� ������� ��
        [SerializeField]// �ӽ� �� �ο�
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private GameObject[] guns;           //��� �ѵ�
        private int currentGunIndex = 0;     //���� ������ ���� �ε���
        private int theNumberOfGun = 2;      //���� ����

        //���� �߻� ����
        private bool isFiring = false;
        //���� �ݱ� ��� ����
        private string keepGunName;

        [SerializeField]
        private Button WeaponChangeButton;
        [SerializeField]
        private Button WeaponGetButton;
        [SerializeField]
        private Button WeaponDeleteButton;

        private void Start()
        {
            //�� ��ü ��Ȱ��ȭ
            foreach(GameObject gun in guns)
            {
                gun.SetActive(false);
            }

            //���� ���� �� Ȱ��ȭ
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //���� ���� ��ư �̺�Ʈ �߰�
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
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName;
        }

        //���� ����
        private void DeleteGun()
        {
            if (keepGunName == null)
                return;

            keepGunName = null;
        }

        //������ ��ȯ
        public float GetDamage()
        {
            return currentGun.Damage;
        }
    }
}