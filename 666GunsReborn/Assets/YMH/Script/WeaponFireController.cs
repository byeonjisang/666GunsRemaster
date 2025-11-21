using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Weapons
{
    public class WeaponFireController : MonoBehaviour
    {
        public List<GameObject> weapons;

        public GameObject KeyGudie;
        private bool isKeyGuideActive = false;

        public Dropdown dropdown;

        public Button fireButton;
        public Toggle toggle;
        private int index = 0;

        private bool isFiring = false;

        private void Start()
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].GetComponent<IWeapon>().Initialization(0, (WeaponID)i);
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
            weapons[index].GetComponent<IWeapon>().StopFire();
        }

        private void Update()
        {
            if (isFiring)
            {
                FireWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isKeyGuideActive = !isKeyGuideActive;
                KeyGudie.SetActive(isKeyGuideActive);
            }

            // Weapon Fire
            if (Input.GetMouseButtonDown(0))
            {
                RequestFire();
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopFireWeapon();
            }

            // Set Toggle
            if (Input.GetMouseButtonDown(1))
            {
                toggle.isOn = !toggle.isOn;
            }

            // Weapon Switch
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchWeapon(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchWeapon(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SwitchWeapon(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SwitchWeapon(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SwitchWeapon(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SwitchWeapon(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SwitchWeapon(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SwitchWeapon(8);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SwitchWeapon(9);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("적 탐색!");
                RaycastHit[] hits = Physics.SphereCastAll(Camera.main.transform.position, 100f, Camera.main.transform.forward, 100f);
                foreach (var hitInfo in hits)
                {
                    Debug.Log("Hit Object: " + hitInfo.collider.name);
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        Debug.Log("Hit Enemy: " + hitInfo.collider.name);
                        hitInfo.collider.GetComponent<Character.Enemy.Enemy>().TakeDamage(50);
                    }
                        
                }
            }
        }

        public void SwitchWeapon(int index)
        {
            if (weapons.Count <= index || index < 0)
            {
                Debug.LogWarning("Invalid weapon index: " + index);
                return;
            }

            Debug.Log("Switching to weapon index: " + index);
            this.index = index;
            dropdown.value = index;
        }
    }
}