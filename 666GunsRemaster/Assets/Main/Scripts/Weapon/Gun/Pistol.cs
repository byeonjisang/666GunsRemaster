using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun 
{
    public class Pistol : Gun
    {
        protected override void PlayFireSound()
        {
            Debug.Log("鼻醚 家府 犁积");
            //鼻醚 醚扁 家府 犁积
            SoundManager.instance.PlayEffectSoundOnce(2);

        }
    }
}