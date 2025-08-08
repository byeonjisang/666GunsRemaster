using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Rifle_Laser : Rifles
    {
        protected override void PlayWeaponSound()
        {
            Debug.Log("Play Rifle_Laser weapon sound");
            SoundManagers.Instance.PlayOneShot(SFX.Pistol_Slide_Fire, transform.position);
        }
    }    
}

