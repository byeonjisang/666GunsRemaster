using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun 
{
    public class Pistol : Gun
    {
        protected override void PlayFireSound()
        {
            Debug.Log("���� �Ҹ� ���");
            //���� �ѱ� �Ҹ� ���
            SoundManager.instance.PlayEffectSoundOnce(2);

        }
    }
}