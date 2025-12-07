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
            GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Enemy", muzzle.position, muzzle.rotation);
            bullet.GetComponent<Weapon.Bullet.BulletBase>().Initialization((float)enemy.EnemyStat.Attack, 10f);
        }
    }
}