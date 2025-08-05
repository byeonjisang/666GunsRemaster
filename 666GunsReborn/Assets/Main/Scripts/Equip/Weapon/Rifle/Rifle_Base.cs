using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Rifle_Base : Rifle_Laser
    {
        protected override void PlayWeaponSound()
        {
            Debug.Log("Play Rifle_Base weapon sound");
        }
    }
}