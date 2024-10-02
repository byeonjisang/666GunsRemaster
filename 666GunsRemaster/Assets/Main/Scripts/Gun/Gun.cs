using Gun.Bullet;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        protected SpriteRenderer playerSprite;
        protected SpriteRenderer sprite;
        protected Button fireButton;
        protected Transform bulletPoint;      //총알 발사 위치
        protected GunData gunData;          // 총의 데이터   

        protected string type;                // 총의 종류
        protected int maxMagazineCount;       // 최대 탄창 속 탄약 크기
        [SerializeField]
        protected int currentMagazineCount;   // 현재 남은 탄창 속 탄약
        protected int maxBullet;              // 최대 탄약량
        [SerializeField]
        protected int currentBulletCount;     // 현재 소유한 탄약량
        protected float reloadTime;           // 재장전 시간
        protected float currentFireRate;      // 남은 딜레이 시간
        protected float fireRate;             // 딜레이
        protected float bulletSpeed;          // 총알 속도
        protected float damage;               // 총알 데미지
        protected float range;                // 사거리
        protected float penetration;          // 관통력
        protected LayerMask blockObject;      // 총알이 맞을 수 없는 레이어
        protected bool isReloading;           // 재장전 중인지
        protected bool isRate;              // 발사 중인지
        protected bool isBullet;              // 총알이 있는지

        protected virtual void Awake()
        {
            playerSprite = GetComponentsInParent<SpriteRenderer>()[1];
            sprite = GetComponent<SpriteRenderer>();
            fireButton = GameObject.Find("FireButton").GetComponent<Button>();
            bulletPoint = GetComponentsInChildren<Transform>()[1];
        }
        protected virtual void Start()
        {
            GunDataInit();
            fireButton.onClick.AddListener(Fire);
        }
        protected void GunDataInit()
        {
            string gunName = gameObject.name;
            gunData = Resources.Load<GunData>("Datas/Gun Data/"+gunName + "Data");
            type = gunData.type;
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
            blockObject = gunData.blockObject;
            isReloading = false;
            isRate = false;
            isBullet = true;
        }
        protected void FixedUpdate()
        {
            if (isRate)
            {
                currentFireRate -= Time.fixedDeltaTime;

                if (currentFireRate <= 0)
                {
                    isRate = false;
                }
            }
        }

        protected void Fire()
        {
            if (isRate || isReloading)
                return;

            //총 발사
            BulletObjectPool.Instance.Spawn(gunData, bulletPoint);
            currentMagazineCount -= 1;      //탄창 속 탄약 감소
            currentFireRate = fireRate;     //현재 발사 딜레이 시간 초기화
            isRate = true;                  //발사 딜레이 시작

            //탄창 속 탄약이 없을 시 재장전
            if (currentMagazineCount <= 0)
            {
                //재장전
                isReloading = true;
                StartCoroutine(Reload());
            }
        }
        protected IEnumerator Reload()
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
            isReloading = false;
        }
    }
}