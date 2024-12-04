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
            //������ �ѱ� �Ҹ� ���
            SoundManager.instance.PlayEffectSound(3);
        }
    }
}

