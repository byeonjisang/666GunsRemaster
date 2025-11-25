using UnityEngine;

namespace Character.Enemy
{
    [CreateAssetMenu(fileName = "Range Attack Strategy", menuName = "Enemy/AttackStrategy/Range")]
    public class RangeAttackStrategy : AttackStrategy
    {
        public GameObject bulletPrefab;

        public override void Execute(Enemy enemy)
        {
            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab is not assigned!");
                return;
            }

            // 총구 위치 가져오기
            Transform muzzle = enemy.ActiveMuzzle[0];

            // 총알 생성
            GameObject bullet = GameObject.Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = muzzle.forward * 10f;
        }
    }
}