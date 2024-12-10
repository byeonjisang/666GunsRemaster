using Character.Player;
using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Gun
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager instance { get; private set; }
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

            monsterScanner = GetComponent<MonsterScannerTest>();
        }

        private MonsterScannerTest monsterScanner;
        public bool IsTarget { get { return monsterScanner.nearestTarget != null; } }

        private Gun currentGun;             //���� ������� ��
        [SerializeField]
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private List<GameObject> guns;       //��� �ѵ�
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
        bool isDie = false;

        //������Ʈ
        private OverhitManager overhitManager;
        private bool[] isOverhit = new bool[2] { false, false };
        [SerializeField]
        private float overhitCount = 100;
        [SerializeField]
        private float overhitTime = 3;

        private void Start()
        {
            //�� ��ü ��Ȱ��ȭ
            foreach (GameObject gun in guns)
            {
                gun.SetActive(false);
            }

            //���� ���� �� Ȱ��ȭ
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //���� ���� ��ư �̺�Ʈ �߰�
            WeaponChangeButton.onClick.AddListener(ChangeGun);
            WeaponGetButton.onClick.AddListener(() => ChangePossessionGuns(keepGunName));
            WeaponDeleteButton.onClick.AddListener(DeleteKeepGun);

            //������Ʈ �ʱ�ȭ
            overhitManager = transform.parent.GetComponentInChildren<OverhitManager>();
            overhitManager.OverhitInit(overhitCount, overhitTime);
            float increaseValue = (100.0f / possessionGuns[0].GetComponent<Gun>().MaxMagazineCount);
            overhitManager.OverhitReset(0, increaseValue);
            increaseValue = (100.0f / possessionGuns[1].GetComponent<Gun>().MaxMagazineCount);
            overhitManager.OverhitReset(1, increaseValue);
        }

        //Ű �Է�
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                StartCoroutine(FireContinuously());
            }
            else
            {
                isFiring = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ChangeGun();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                ChangePossessionGuns(keepGunName);
            }
        }

        private void FixedUpdate()
        {
            RotateWeaponTowardsTarget();
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
            StopCoroutine(FireContinuously());
        }
        private IEnumerator FireContinuously()
        {
            if (isDie)
                yield return null;

            isFiring = true;
            float fireRate = currentGun.FireRate;
            while (isFiring)
            {
                if (isOverhit[currentGunIndex])
                {
                    yield return null;
                }
                else if (currentGun != null)
                {
                    currentGun.Fire();
                }
                yield return new WaitForSeconds(fireRate);
            }
        }

        public void SetIsOverhit(int weaponIndex, bool isOverhit)
        {
            this.isOverhit[weaponIndex] = isOverhit;
        }

        //���� ����
        private void ChangeGun()
        {
            if (theNumberOfGun < 2 && isDie)
                return;

            currentGunIndex = 1 - currentGunIndex;

            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();
            possessionGuns[1 - currentGunIndex].SetActive(false);
            //�÷��̾���Ʈ�ѷ��� ���� ���� �ε��� ����
            PlayerController.Instance.CurrentWeaponIndex = currentGunIndex;

            //����Ʈ ȿ��

            //����
        }

        //������ ���� ��ü
        public void ChangePossessionGuns(string gunName)
        {
            if (gunName == null && isDie)
                return;

            GameObject gunObject = guns.Find(gun => gun.name == gunName && !possessionGuns.Contains(gun));

            possessionGuns[currentGunIndex].SetActive(false);
            possessionGuns[currentGunIndex] = gunObject;
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();
            //UI ����
            UIManager.Instance.UpdateGetWeaponImage(null);

            //Overhit ������ ���
            float increaseValue = (100.0f / currentGun.MaxMagazineCount);
            //Overhit �ʱ�ȭ
            overhitManager.OverhitReset(currentGunIndex, increaseValue);

            if (gunName != "Pistol")
                keepGunName = null;
        }

        //���� ���� ȹ��
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName.Replace("(Clone)", "");

            //�̹��� ����
            Gun gunObject = guns.Find(gun => gun.name == this.keepGunName).GetComponent<Gun>();
            Sprite gunImage = gunObject.gunUiImage;
            UIManager.Instance.UpdateGetWeaponImage(gunImage);
        }

        //���� ���� ����
        private void DeleteKeepGun()
        {
            if (keepGunName == null && isDie)
                return;

            keepGunName = null;
            UIManager.Instance.UpdateGetWeaponImage(null);
        }

        //���� ���� �⺻������ ����
        private void ChangeKeepGunDefault()
        {

        }

        //������ ��ȯ
        public float GetDamage()
        {
            return currentGun.Damage;
        }

        public void DeleteAllWeapon()
        {
            isDie = true;
            foreach (GameObject gun in possessionGuns)
            {
                gun.SetActive(false);
            }
        }

        private void RotateWeaponTowardsTarget()
        {
            if (monsterScanner.nearestTarget != null)
            {
                Debug.Log("���� ����");

                Vector3 targetDirection = monsterScanner.nearestTarget.position - transform.position;

                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 270;
                if (targetDirection.x < 0)
                {
                    PlayerController.Instance.ReversePlayer(false);
                    transform.localScale = new Vector3(1, 1, 1);

                    if (angle > 360)
                        angle -= 360;
                }
                else
                {
                    PlayerController.Instance.ReversePlayer(true);
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
            else
            {
                if (transform.localScale.x == -1)
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                else
                    transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }

        public void BulletCountUp(int value)
        {
            currentGun.BulletCountUp(value);
        }

        public void IncreaseOverhit(float increaseValue)
        {
            overhitManager.IncreaseOverhitGauge(currentGunIndex, increaseValue);
        }
    }
}