using UnityEngine;

namespace Gun 
{
    public class Pistol : Gun
    {
        protected override void PlayFireSound()
        {
            //���� �ѱ� �Ҹ� ���
            SoundManager.instance.PlayEffectSoundOnce(2);

        }
    }
}