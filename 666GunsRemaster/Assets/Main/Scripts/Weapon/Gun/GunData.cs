using System.Collections;
using UnityEngine;

namespace Gun
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/GunData", order = 0)]
    public class GunData : ScriptableObject
    {
        public int index;                   // 총의 인덱스
        public int maxMagazineCount;       // 최대 탄창 속 탄약 크기
        public int maxBullet;              // 최대 탄약량
        public float reloadTime;           // 재장전 시간
        public float fireRate;             // 딜레이
        public float bulletSpeed;          // 총알 속도
        public float damage;               // 총알 데미지
        public float range;                // 사거리
        public float penetration;          // 관통력
        public float shotgunSpread;        // 샷건 탄착 각도
        public LayerMask blockObject;      // 총알이 맞을 수 없는 레이어
    }
}