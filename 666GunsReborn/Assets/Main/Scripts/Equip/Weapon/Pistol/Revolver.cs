using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Revolver : PistolBase
    {
        protected override void PlayFireSound()
        {
            base.PlayFireSound();
            // Play revolver fire sound here
            Debug.Log("Revolver fire sound played");
            //SoundManagers.Instance.PlayOneShot(SFX.Reveolver_Fire, transform.position);
        } 
    }
}
