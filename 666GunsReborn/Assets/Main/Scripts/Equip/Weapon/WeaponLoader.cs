using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    [SerializeField]
    private Transform weaponParent;

    public Weapon LoadWeapon(int index, WeaponType weaponType)
    {
        GameObject weaponPrefab = Resources.Load<GameObject>($"Weapons/{weaponType}");
        if (weaponPrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, weaponParent);
            weaponInstance.name = weaponType.ToString();

            Weapon weaponComponent = weaponInstance.GetComponent<Weapon>();
            weaponComponent.Initialized(index, weaponType);

            if (index != 0)
            {
                weaponInstance.SetActive(false);
            }

            return weaponComponent;
        }
        else
        {
            Debug.LogError($"Weapon prefab for {weaponType} not found in Resources/Weapons.");
            return null;
        }
    }
}
