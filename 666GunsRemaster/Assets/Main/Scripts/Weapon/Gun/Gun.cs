using Character.Player;
using Gun.Bullet;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        protected BulletObjectPool bulletObjectPool; //총알 오브젝트 풀

        public Sprite gunUiImage; //총 이미지

        protected SpriteRenderer playerSprite;
        protected SpriteRenderer sprite;
        protected Button fireButton;
        protected EventTrigger eventTrigger;
        protected GameObject bulletPoint;      //총알 발사 위치
        protected GunData gunData;          // 총의 데이터   

        protected int gunIndex;               // 총의 인덱스
        protected int maxMagazineCount;       // 최대 탄창 속 탄약 크기
        [Header("남은 총알의 갯수들")]
        protected int currentMagazineCount;   // 현재 남은 탄창 속 탄약
        protected int maxBullet;              // 최대 탄약량
        protected int currentBulletCount;     // 현재 소유한 탄약량
        protected float reloadTime;           // 재장전 시간
        protected float currentFireRate;      // 남은 딜레이 시간
        public float FireRate { get { return fireRate; } }
        protected float fireRate;             // 딜레이
        protected float bulletSpeed;          // 총알 속도
        public float Damage { get { return damage; } }
        protected float damage;               // 총알 데미지
        protected float range;                // 사거리
        protected float penetration;          // 관통력
        protected float shotgunSpread;        // 샷건 탄착 각도
        protected LayerMask blockObject;      // 총알이 맞을 수 없는 레이어

        protected bool isReloading;           // 재장전 중인지
        protected bool isRate;              // 발사 중인지
        protected bool isBullet;              // 총알이 있는지

        protected virtual void Awake()
        {
            playerSprite = GetComponentsInParent<SpriteRenderer>()[1];
            sprite = GetComponent<SpriteRenderer>();
            fireButton = GameObject.Find("FireButton").GetComponent<Button>();
            eventTrigger = fireButton.GetComponent<EventTrigger>();
            bulletPoint = transform.GetChild(0).gameObject;
        }
        protected virtual void Start()
        {
            GunDataInit();
        }
        protected virtual void GunDataInit()
        {
            string gunName = gameObject.name;
            gunData = Resources.Load<GunData>("Datas/Gun Data/"+gunName + "Data");
            gunIndex = gunData.index;
            maxMagazineCount = gunData.maxMagazineCount;
            currentMagazineCount = maxMagazineCount;
            maxBullet = gunData.maxBullet;
            currentBulletCount = maxBullet;
            reloadTime = gunData.reloadTime;
            fireRate = gunData.fireRate;
            bulletSpeed = gunData.bulletSpeed;
            damage = gunData.damage;
            range = gunData.range;
            penetration = gunData.penetration;
            shotgunSpread = gunData.shotgunSpread;
            blockObject = gunData.blockObject;
            isReloading = false;
            isRate = false;
            isBullet = true;
        }
        protected virtual void OnEnable()
        {
            StartCoroutine(UpdateWeaponUI());
            StartCoroutine(Reload());
        }
        protected virtual void OnDisable()
        {
            if(isReloading)
                StopCoroutine(Reload());
            isRate = false;
        }
        protected virtual IEnumerator UpdateWeaponUI()
        {
            yield return null;
            UIManager.Instance.UpdateBulletCount(currentBulletCount, currentMagazineCount); //UI 총알 갱신
            UIManager.Instance.UpdateWeaponImage(gunUiImage);                            //UI 총 이미지 갱신
        }
        protected virtual void Update()
        {
            //총 발사 딜레이 시간 계산
            if (isRate)
            {
                currentFireRate -= Time.deltaTime;

                if (currentFireRate <= 0)
                {
                    isRate = false;
                }
            }
        }

        public virtual void Fire()
        {
            if (isRate || isReloading)
                return;

            PlayerController.Instance.OverHit();
            bulletPoint.transform.localScale = new Vector3(playerSprite.flipX ? -1 : 1, 1, 1); //총알 발사 방향 설정

            //총 발사
            bulletObjectPool.GetBullet(bulletPoint.transform);
            currentMagazineCount -= 1;      //탄창 속 탄약 감소
            currentFireRate = fireRate;     //현재 발사 딜레이 시간 초기화
            isRate = true;                  //발사 딜레이 시작
            UIManager.Instance.UpdateBulletCount(currentBulletCount, currentMagazineCount); //UI 갱신
            PlayFireSound();                //총기 사운드 재생

            //총기 발사 사운드

            //탄창 속 탄약이 없을 시 재장전
            if (currentMagazineCount <= 0)
            {
                //총알이 없으면 기본 권총으로 변경
                if (currentBulletCount == 0)
                {
                    WeaponManager.instance.ChangePossessionGuns("Pistol");
                    //총기 변경 사운드

                    return;
                }

                //재장전
                isReloading = true;
                //재장전 사운드

                StartCoroutine(Reload());
            }
        }
        //총기 발사 사운드 재생
        protected virtual void PlayFireSound()
        {
            //총기 발사 소리
            Debug.Log("총기 발사 소리 재생");
            //여기는 코드 작성 안함
        }

        protected virtual IEnumerator Reload()
        {
            yield return new WaitForSeconds(reloadTime);

            //현재 소유한 탄약이 탄창 속 탄약보다 많을 시
            if (currentBulletCount >= maxMagazineCount - currentMagazineCount)
            {
                currentBulletCount -= maxMagazineCount - currentMagazineCount;

                currentMagazineCount = maxMagazineCount;    
            }
            //현재 소유한 탄약이 탄창 속 탄약보다 적을 시
            else
            {
                currentMagazineCount += currentBulletCount;

                currentBulletCount = 0;
            }
            UIManager.Instance.UpdateBulletCount(currentBulletCount, currentMagazineCount); //UI 갱신
            isReloading = false;
        }

        public void BulletCountUp(int value)
        {
            currentBulletCount += value;
            UIManager.Instance.UpdateBulletCount(currentBulletCount, currentMagazineCount);
        }
    }
}