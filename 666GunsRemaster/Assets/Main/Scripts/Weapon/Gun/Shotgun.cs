using Character.Player;
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

            WeaponManager.instance.IncreaseOverhit((100.0f / maxMagazineCount) * 3);
            bulletPoint.transform.localScale = new Vector3(playerSprite.flipX ? -1 : 1, 1, 1); //�Ѿ� �߻� ���� ����

            //�� �߻�
            for(int i = -1; i < 2; i++)
            {
                bulletObjectPool.GetBullet(bulletPoint.transform, i);
                currentMagazineCount -= 1;      //źâ �� ź�� ����
            }
            currentFireRate = fireRate;     //���� �߻� ������ �ð� �ʱ�ȭ
            isRate = true;                  //�߻� ������ ����
            UIManager.Instance.UpdateBulletCount(currentBulletCount, currentMagazineCount); //UI ����
            
            //���� ����
            SoundManager.instance.PlayEffectSoundOnce(4);


            //źâ �� ź���� ���� �� ������
            if (currentMagazineCount <= 0)
            {
                //�Ѿ��� ������ �⺻ �������� ����
                if (currentBulletCount == 0)
                {
                    WeaponManager.instance.ChangePossessionGuns("Pistol");
                    //�ѱ� ���� ����

                    return;
                }

                //������
                isReloading = true;
                Debug.Log(isReloading);
                //������ ����

                StartCoroutine(Reload());
            }
        }
    }
}

