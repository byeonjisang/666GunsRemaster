using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun.Bullet 
{
    public class ShotgunBullet : Bullet
    {
        protected float shotgunSpread;        // ¼¦°Ç ÅºÂø °¢µµ

        protected override void Start()
        {
            base.Start();
            shotgunSpread = gunData.shotgunSpread;
        }
        public override void Shoot(int index)
        {
            StartCoroutine(ShootBullet(index));
        }
        protected override IEnumerator ShootBullet(int index)
        {
            yield return null;

            float angle = index * (shotgunSpread / 2);
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);

            reverse = transform.localScale.x == 1 ? -1 : 1;
            rigid.AddForce(transform.right * speed * reverse, ForceMode2D.Impulse);
        }
    }
}