using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Revolver : Pistols
    {
        protected override void PlayWeaponSound()
        {
            base.PlayWeaponSound();
            // Play revolver fire sound here
            Debug.Log("Revolver fire sound played");
            //SoundManagers.Instance.PlayOneShot(SFX.Reveolver_Fire, transform.position);
        } 
    }
}
