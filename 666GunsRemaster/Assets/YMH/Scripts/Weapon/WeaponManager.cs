using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gun
{
    public class WeaponManager : MonoBehaviour
    {
        private Gun currentBullet;   //���� ������� �Ѿ�
        [SerializeField]// �ӽ� �� �ο�
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private Button WeaponChangeButton;

        private int currentWeaponIndex = 0;     //���� ������ ���� �ε���

        private void Start()
        {
            //�������� �ʴ� �� ��Ȱ��ȭ
            possessionGuns[1 - currentWeaponIndex].SetActive(false);
            currentBullet = possessionGuns[currentWeaponIndex].GetComponent<Gun>();

            //���� ���� ��ư �̺�Ʈ �߰�
            WeaponChangeButton.onClick.AddListener(ChangeWeapon);
        }

        //���� ����
        private void ChangeWeapon()
        {
            currentWeaponIndex = 1 - currentWeaponIndex;

            possessionGuns[currentWeaponIndex].SetActive(true);
            currentBullet = possessionGuns[currentWeaponIndex].GetComponent<Gun>();
            possessionGuns[1 - currentWeaponIndex].SetActive(false);

            //����Ʈ ȿ��
        }
    }
}