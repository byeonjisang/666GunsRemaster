using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Character.Gun.Bullet
{
    public class BulletObjectPool : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public int maxPoolSize = 20;
        public int stackDefalutCapcity = 20;

        public IObjectPool<Bullet> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<Bullet>(
                        CreatedPooledItem,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefalutCapcity,
                        maxPoolSize
                        );

                return _pool;
            }
        }
        private IObjectPool<Bullet> _pool;

        private Bullet CreatedPooledItem()
        {
            var go = Instantiate(bulletPrefab);

            Bullet bullet = go.GetComponent<Bullet>();

            go.name = "Bullet";
            bullet.Pool = Pool;

            return bullet;
        }

        private void OnReturnedToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        
        private void OnTakeFromPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        public void Spawn()
        {

        }
    }
}