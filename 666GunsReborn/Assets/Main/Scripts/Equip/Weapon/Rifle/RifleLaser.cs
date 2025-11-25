using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class RifleLaser : RifleBase
    {
        protected override void PlayFireSound()
        {
            Debug.Log("Play Rifle_Laser weapon sound");
            SoundManagers.Instance.PlayOneShot(SFX.Pistol_Slide_Fire, transform.position);
        }
    }    
}

