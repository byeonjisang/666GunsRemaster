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

        private Gun currentGun;             //현재 사용중인 총
        [SerializeField]
        private GameObject[] possessionGuns = new GameObject[2];
        [SerializeField]
        private List<GameObject> guns;       //모든 총들
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
        bool isDie = false;

        //오버히트
        private OverhitManager overhitManager;
        private bool[] isOverhit = new bool[2] { false, false };
        [SerializeField]
        private float overhitCount = 100;
        [SerializeField]
        private float overhitTime = 3;

        private void Start()
        {
            //총 전체 비활성화
            foreach (GameObject gun in guns)
            {
                gun.SetActive(false);
            }

            //착용 중인 총 활성화
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();

            //무기 변경 버튼 이벤트 추가
            WeaponChangeButton.onClick.AddListener(ChangeGun);
            WeaponGetButton.onClick.AddListener(() => ChangePossessionGuns(keepGunName));
            WeaponDeleteButton.onClick.AddListener(DeleteKeepGun);

            //오버히트 초기화
            overhitManager = transform.parent.GetComponentInChildren<OverhitManager>();
            overhitManager.OverhitInit(overhitCount, overhitTime);
            float increaseValue = (100.0f / possessionGuns[0].GetComponent<Gun>().MaxMagazineCount);
            overhitManager.OverhitReset(0, increaseValue);
            increaseValue = (100.0f / possessionGuns[1].GetComponent<Gun>().MaxMagazineCount);
            overhitManager.OverhitReset(1, increaseValue);
        }

        //키 입력
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

        //무기 변경
        private void ChangeGun()
        {
            if (theNumberOfGun < 2 && isDie)
                return;

            currentGunIndex = 1 - currentGunIndex;

            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();
            possessionGuns[1 - currentGunIndex].SetActive(false);
            //플레이어컨트롤러에 현재 무기 인덱스 저장
            PlayerController.Instance.CurrentWeaponIndex = currentGunIndex;

            //임팩트 효과

            //사운드
        }

        //소지한 무기 교체
        public void ChangePossessionGuns(string gunName)
        {
            if (gunName == null && isDie)
                return;

            GameObject gunObject = guns.Find(gun => gun.name == gunName && !possessionGuns.Contains(gun));

            possessionGuns[currentGunIndex].SetActive(false);
            possessionGuns[currentGunIndex] = gunObject;
            possessionGuns[currentGunIndex].SetActive(true);
            currentGun = possessionGuns[currentGunIndex].GetComponent<Gun>();
            //UI 변경
            UIManager.Instance.UpdateGetWeaponImage(null);

            //Overhit 증가량 계산
            float increaseValue = (100.0f / currentGun.MaxMagazineCount);
            //Overhit 초기화
            overhitManager.OverhitReset(currentGunIndex, increaseValue);

            if (gunName != "Pistol")
                keepGunName = null;
        }

        //보관 무기 획득
        public void KeepGun(string keepGunName)
        {
            this.keepGunName = keepGunName.Replace("(Clone)", "");

            //이미지 변경
            Gun gunObject = guns.Find(gun => gun.name == this.keepGunName).GetComponent<Gun>();
            Sprite gunImage = gunObject.gunUiImage;
            UIManager.Instance.UpdateGetWeaponImage(gunImage);
        }

        //보관 무기 삭제
        private void DeleteKeepGun()
        {
            if (keepGunName == null && isDie)
                return;

            keepGunName = null;
            UIManager.Instance.UpdateGetWeaponImage(null);
        }

        //보관 무기 기본값으로 변경
        private void ChangeKeepGunDefault()
        {

        }

        //데미지 반환
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
                Debug.Log("몬스터 감지");

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