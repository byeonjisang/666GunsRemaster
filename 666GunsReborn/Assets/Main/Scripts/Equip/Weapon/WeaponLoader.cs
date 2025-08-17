using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class WeaponLoader : MonoBehaviour
{
    [SerializeField]
    private Transform weaponParent;

    // 과거에 사용된 코드(지울 예정)
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

    // 무기 리펙토링 버전
    public IWeapon LoadWeapon(int index, WeaponID weaponID)
    {
        GameObject weaponPrefab = Resources.Load<GameObject>($"Weapons/{weaponID}");
        if (weaponPrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, weaponParent);
            weaponInstance.name = weaponID.ToString();

            IWeapon weaponComponent = weaponInstance.GetComponent<IWeapon>();
            weaponComponent.Initialization(index, weaponID);

            // 인덱스 0이 아니면 비활성화(메인 무기X)
            if (index != 0)
                weaponInstance.SetActive(false);

            return weaponComponent;
        }
        else
        {
            Debug.LogError($"Weapon Prefab for {weaponID} not found in Resources/Weapons.");
            return null;
        }
    }
}
