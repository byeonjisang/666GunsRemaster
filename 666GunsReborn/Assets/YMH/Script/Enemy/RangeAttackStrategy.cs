using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Range Attack Strategy", menuName = "Enemy/AttackStrategy/Range")]
    public class RangeAttackStrategy : AttackStrategy
    {
        public GameObject bulletPrefab;

        public override void Execute(Enemy enemy)
        {
            if (bulletPrefab == null)
                return;

            // 발사 위치 (예: 적의 앞쪽 1미터)
            Vector3 spawnPosition = enemy.transform.position + enemy.transform.forward * 1f;
            
            // 발사체를 생성하고 힘을 가함
            GameObject projectile = Instantiate(bulletPrefab, spawnPosition, enemy.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(enemy.transform.forward, ForceMode.Impulse);
            Debug.Log("원거리 공격!");
        }
    }
}