using UnityEngine;
using System.Collections.Generic;
using Weapon;
using Unity.VisualScripting;

namespace Character.Enemy
{
    public class ThrowEnemy : Enemy
    {   
        [Header("Bottle Bomb")]
        // 현재 들고 있는 병폭탄
        public Bottle _equipBottle;
        // 병이 날아가는 속도
        [SerializeField] private float _throwSpeed = 5f;

        protected override void Awake()
        {
            base.Awake();

            // 처음 들고 있는 병폭탄 탐색
            _equipBottle = FindFirstObjectByType<Bottle>();
            // 병폭탄 초기화
            _equipBottle.Init();
        }

        public override void Attack()
        {
            
        }

        /// <summary>
        /// 병 폭탄 투척
        /// </summary>
        public void ThrowBottle()
        {
            // 병 폭탄 투척
            _equipBottle.Throw(PlayerTransform, _throwSpeed);
            // 병 제외
            _equipBottle = null;

            // 병 재장전
            //ReloadBottle();
        }

        // 병폭탄 재장전
        public void ReloadBottle()
        {
            // 병폭탄 소환
            GameObject bottle = Instantiate(_enemyData.WeaponPrefab, transform);
            bottle.transform.SetParent(_weaponSocket, false);
            bottle.transform.localPosition = Vector3.zero;
            bottle.transform.localRotation = Quaternion.identity;

            // 병폭탄 초기화
            _equipBottle = bottle.GetComponent<Bottle>();
            _equipBottle.Init();
        }
    }
}