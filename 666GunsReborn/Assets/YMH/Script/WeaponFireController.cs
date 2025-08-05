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
        private int index = 0;

        private void Start()
        {
            foreach (var weapon in weapons)
            {
                string path = $"Datas/Weapon/Pistol/{weapon.name}";
                WeaponData weaponData = Resources.Load<WeaponData>(path);
                Debug.Log($"{weapon.name} Initialization with data from {path}");
                if (weaponData == null)
                {
                    Debug.LogError($"WeaponData not found at {path}");
                    continue;
                }
                weapon.GetComponent<IWeapon>().Initialization(0, weaponData);
            }
        }

        public void FireWeapon()
        {
            weapons[index].GetComponent<IWeapon>().Fire();
        }

        public void SwitchWeapon(int index)
        {
            Debug.Log("Switching to weapon index: " + index);
            this.index = index;
        }
    }
}