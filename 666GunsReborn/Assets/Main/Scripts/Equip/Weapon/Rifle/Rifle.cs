using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Rifle : RifleBase
    {
        protected override void PlayFireSound()
        {
            Debug.Log("Play Rifle_Base weapon sound");
            SoundManagers.Instance.PlayOneShot(SFX.Pistol_Slide_Fire, transform.position);
        }
    }
}