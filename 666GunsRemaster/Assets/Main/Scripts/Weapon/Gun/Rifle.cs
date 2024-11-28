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
            Debug.Log("라이플 소리 재생");
            //라이플 총기 소리 재생
            SoundManager.instance.PlayEffectSound(3);
        }
    }
}

