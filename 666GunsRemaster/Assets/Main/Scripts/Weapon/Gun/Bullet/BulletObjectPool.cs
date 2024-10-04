
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Gun.Bullet
{
    public class BulletObjectPool : MonoBehaviour
    {
        public static BulletObjectPool Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }   //싱글톤

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

            string bulletName = go.name.Replace("(Clone)", "").Trim();
            Type bulletType = Type.GetType("Gun.Bullet." + bulletName);
            Bullet bullet = (Bullet)go.AddComponent(bulletType);

            go.name = bulletPrefab.name;
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

        ///<summary>
        ///총알 생성
        ///</summary>
        ///<param name="gunData"> 총의 데이터</param>
        ///<param name="bulletPoint"> 총알 생성 위치</param>
        public void Spawn(GunData gunData, Transform bulletPoint)
        {
            var bullet = Pool.Get();
            bullet.gunData = gunData;
            bullet.transform.position = bulletPoint.position;
            bullet.transform.rotation = bulletPoint.rotation;
            bullet.transform.localScale = bulletPoint.localScale;
            bullet.transform.parent = this.transform;
        }
    }
}