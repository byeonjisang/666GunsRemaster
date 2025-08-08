using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Weapons
{
    public class WeaponFireController : MonoBehaviour
    {
        public List<GameObject> weapons;

        public Button fireButton;
        public Toggle toggle;
        private int index = 0;

        private bool isFiring = false;

        private void Start()
        {
            foreach (var weapon in weapons)
            {
                weapon.GetComponent<IWeapon>().Initialization(0, (WeaponID)index);
            }
        }

        public void RequestFire()
        {
            if (toggle.isOn)
            {
                isFiring = true;
            }
            else
            {
                isFiring = false;
                FireWeapon();
            }
        }

        public void FireWeapon()
        {
            weapons[index].GetComponent<IWeapon>().Fire();
        }

        public void StopFireWeapon()
        {
            isFiring = false;
        }

        private void Update()
        {
            if (isFiring)
            {
                FireWeapon();
            }
        }

        public void SwitchWeapon(int index)
        {
            Debug.Log("Switching to weapon index: " + index);
            this.index = index;
        }
    }
}