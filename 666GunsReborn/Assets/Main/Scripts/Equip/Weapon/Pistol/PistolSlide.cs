using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class PistolSlide : PistolBase
    {
        protected override void PlayFireSound()
        {
            base.PlayFireSound();
            // Play pistol slide fire sound here
            Debug.Log("Pistol slide fire sound played");
            //SoundManagers.Instance.PlayOneShot(SFX.Pistol_Slide_Fire, transform.position);
        }
    }
}
