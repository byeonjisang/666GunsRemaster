<<<<<<<< HEAD:666GunsReborn/Assets/Main/Scripts/Character/Enemy/Old Script/Enemy/DronAttackStrategy.cs
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Drone Attack Strategy", menuName = "Enemy/AttackStrategy/Drone")]
    public class DroneAttackStrategy : AttackStrategy
    {
        public GameObject bulletPrefab;
        int muzzleIndex = 0;

        public override void Execute(Enemy enemy)
        {
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab is not assigned!");
                return;
            }

            // 총구 위치 가져오기
            List<Transform> muzzles = enemy.ActiveMuzzle;

            // 총알 생성
            Transform muzzle = muzzles[muzzleIndex];
            muzzleIndex = 1 - muzzleIndex;

            GameObject bullet = GameObject.Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = muzzle.forward * 10f;
        }
    }
========
using System.Collections.Generic;
using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "Drone Attack Strategy", menuName = "Enemy/AttackStrategy/Drone")]
    public class DroneAttackStrategy : AttackStrategy
    {
        public GameObject bulletPrefab;
        int muzzleIndex = 0;

        public override void Execute(Enemy enemy)
        {
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab is not assigned!");
                return;
            }

            // 총구 위치 가져오기
            List<Transform> muzzles = enemy.ActiveMuzzle;

            // 총알 생성
            Transform muzzle = muzzles[muzzleIndex];
            muzzleIndex = 1 - muzzleIndex;

            GameObject bullet = GameObject.Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = muzzle.forward * 10f;
        }
    }
>>>>>>>> origin/main:666GunsReborn/Assets/Main/Scripts/Character/Enemy/State/DronAttackStrategy.cs
}