using UnityEngine;

namespace Character.Enemy
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private GameObject _bulletPrefab;
        public override void Attack()
        {

        }
        
        /// <summary>
        /// 총알 발사
        /// 애니메이션에 이벤트 걸어서 사용하기 위해 따로 분리
        /// </summary>
        public void Shoot()
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