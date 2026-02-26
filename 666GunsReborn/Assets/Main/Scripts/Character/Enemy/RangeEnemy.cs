using UnityEngine;

namespace Character.Enemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private GameObject _bulletPrefab;
        public override void Attack()
        {
            if (_bulletPrefab == null)
            {
                Debug.LogError("Bullet Prefab is not assigned!");
                return;
            }

            // 총구 위치 가져오기
            Transform muzzle = ActiveMuzzle[0];

            // 총알 생성
            GameObject bullet = ObjectPoolManager.Instance.GetFromPool("Bullet_Enemy", muzzle.position, muzzle.rotation);
            bullet.GetComponent<Weapon.Bullet.BulletBase>().Init((float)EnemyStat.Attack, 10f);
        }
    }
}