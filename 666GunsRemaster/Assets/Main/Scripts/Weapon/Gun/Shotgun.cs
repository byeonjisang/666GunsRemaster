using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Shotgun : Gun
    {
        public override void Fire()
        {
            if (isRate || isReloading)
                return;

            bulletPoint.transform.localScale = new Vector3(playerSprite.flipX ? -1 : 1, 1, 1); //ÃÑ¾Ë ¹ß»ç ¹æÇâ ¼³Á¤

            //ÃÑ ¹ß»ç
            for(int i = -1; i < 2; i++)
            {
                bulletObjectPool.GetBullet(bulletPoint.transform, i);
                currentMagazineCount -= 1;      //ÅºÃ¢ ¼Ó Åº¾à °¨¼Ò
            }
            currentFireRate = fireRate;     //ÇöÀç ¹ß»ç µô·¹ÀÌ ½Ã°£ ÃÊ±âÈ­
            isRate = true;                  //¹ß»ç µô·¹ÀÌ ½ÃÀÛ

            //ÅºÃ¢ ¼Ó Åº¾àÀÌ ¾øÀ» ½Ã ÀçÀåÀü
            if (currentMagazineCount == 0)
            {
                //ÃÑ¾ËÀÌ ¾øÀ¸¸é ±âº» ±ÇÃÑÀ¸·Î º¯°æ
                if (currentBulletCount == 0)
                {
                    WeaponManager.instance.ChangePossessionGuns("Pistol");
                    return;
                }

                //ÀçÀåÀü
                isReloading = true;
                StartCoroutine(Reload());
            }
        }
    }
}

