using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private Gun currentGun;   //���� ������� ��
        [SerializeField]// �ӽ� �� �ο�
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private GameObject[] guns;           //��� �ѵ�
        private int currentGunIndex = 0;     //���� ������ ���� �ε���
        private int theNumberOfGun = 2;      //���� ����

        //���� �ݱ� ��� ����
        private string keepGunName;

        [SerializeField]
        private Button WeaponChangeButton;
        [SerializeField]
        private Button WeaponGetButton;
        [SerializeField]
        private Button GetLockButton;

        private void Start()
        {
            //���� ���� �� Ȱ��ȭ
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //���� ���� ��ư �̺�Ʈ �߰�
            WeaponChangeButton.onClick.AddListener(ChangeGun);
            WeaponGetButton.onClick.AddListener(GetGun);
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
        //���� �ݱ�
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName;
        }
        //���� ȹ��
        private void GetGun()
        {
            if (keepGunName == null)
                return;

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

        }

        //������ ��ȯ
        public float GetDamage()
        {
            return currentGun.Damage;
        }
    }
}