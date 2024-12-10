using UnityEngine;

namespace Gun 
{
    public class Pistol : Gun
    {
        protected override void PlayFireSound()
        {
            //±ÇÃÑ ÃÑ±â ¼Ò¸® Àç»ý
            SoundManager.instance.PlayEffectSoundOnce(2);

        }
    }
}