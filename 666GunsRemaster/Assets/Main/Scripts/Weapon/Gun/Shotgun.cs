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

            bulletPoint.transform.localScale = new Vector3(playerSprite.flipX ? -1 : 1, 1, 1); //총알 발사 방향 설정

            //총 발사
            for(int i = -1; i < 2; i++)
            {
                bulletObjectPool.GetBullet(bulletPoint.transform, i);
                currentMagazineCount -= 1;      //탄창 속 탄약 감소
            }
            currentFireRate = fireRate;     //현재 발사 딜레이 시간 초기화
            isRate = true;                  //발사 딜레이 시작
            //샷건 사운드


            //탄창 속 탄약이 없을 시 재장전
            if (currentMagazineCount == 0)
            {
                //총알이 없으면 기본 권총으로 변경
                if (currentBulletCount == 0)
                {
                    WeaponManager.instance.ChangePossessionGuns("Pistol");
                    //총기 변경 사운드

                    return;
                }

                //재장전
                isReloading = true;
                //재장전 사운드

                StartCoroutine(Reload());
            }
        }
    }
}

