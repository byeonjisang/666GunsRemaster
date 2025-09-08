using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Pistol_Slide : Pistols
    {
        protected override void PlayWeaponSound()
        {
            base.PlayWeaponSound();
            // Play pistol slide fire sound here
            Debug.Log("Pistol slide fire sound played");
            //SoundManagers.Instance.PlayOneShot(SFX.Pistol_Slide_Fire, transform.position);
        }
    }
}
