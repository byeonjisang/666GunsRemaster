using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Rifle : Gun
    {
        protected override void PlayFireSound()
        {
            Debug.Log("������ �Ҹ� ���");
            //������ �ѱ� �Ҹ� ���
            SoundManager.instance.PlayEffectSound(3);
        }
    }
}

