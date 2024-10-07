using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gun.Bullet
{
    public class BulletObjectPool : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public int poolSize = 50;

        [SerializeField]
        private GunData gunData;
        private Queue<GameObject> bulletPool;

        private void Awake()
        {
            bulletPool = new Queue<GameObject>();

            // 초기 풀 생성
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.parent = this.transform;

                string bulletName = bullet.name.Replace("(Clone)", "").Trim();
                Type bulletType = Type.GetType("Gun.Bullet." + bulletName);
                bullet.AddComponent(bulletType);

                bullet.GetComponent<Bullet>().gunData = gunData;

                bullet.SetActive(false);
                bulletPool.Enqueue(bullet);
            }
        }

        ///<summary>
        ///총알 생성
        ///</summary>
        ///<param name="gunData"> 총의 데이터</param>
        ///<param name="bulletPoint"> 총알 생성 위치</param>
        // 총알을 풀에서 가져오는 메서드
        public void GetBullet(Transform bulletPoint)
        {
            GameObject bullet;
            if (bulletPool.Count > 0)
            {
                bullet = bulletPool.Dequeue();
                bullet.SetActive(true);
            }
            else
            {
                // 풀에 여유가 없으면 새로운 총알을 생성
                bullet = Instantiate(bulletPrefab);
                bullet.transform.parent = this.transform;

                // 총알의 이름을 이용하여 총알의 타입을 동적으로 가져옴
                string bulletName = bullet.name.Replace("(Clone)", "").Trim();
                Type bulletType = Type.GetType("Gun.Bullet." + bulletName);
                bullet.AddComponent(bulletType);

                bullet.GetComponent<Bullet>().gunData = gunData;
            }

            bullet.transform.position = bulletPoint.position;
            bullet.transform.rotation = bulletPoint.rotation;
            bullet.transform.localScale = bulletPoint.localScale;
            bullet.transform.parent = this.transform;

            bullet.GetComponent<Bullet>().Shoot();
        }

        // 총알을 풀에 다시 반환하는 메서드
        public void ReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
}