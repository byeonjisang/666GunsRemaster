﻿using System.Collections;
using UnityEngine;

namespace Gun
{
    [CreateAssetMenu(fileName = "GunData", menuName = "GunData", order = 0)]
    public class GunData : ScriptableObject
    {
        public string name;                // 총의 이름 
        public string type;                // 총의 종류
        public int maxMagazineCount;       // 최대 탄창 속 탄약 크기
        public int maxBullet;              // 최대 탄약량
        public float reloadTime;           // 재장전 시간
        public float fireRate;             // 딜레이
        public float bulletSpeed;          // 총알 속도
        public float damage;               // 총알 데미지
        public float range;                // 사거리
        public float penetration;          // 관통력
        public LayerMask blockObject;      // 총알이 맞을 수 없는 레이어
    }
}