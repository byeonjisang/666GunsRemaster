using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField]
    private Dropdown[] weaponTypeDropdowns = new Dropdown[2];

    private void Start()
    {
        weaponTypeDropdowns[0].value = (int)WeaponType.Pistol;
        //weaponTypeDropdowns[1].value = (int)WeaponType.Rifle;

        WeaponManager.Instance.Initialized((int)WeaponType.Pistol);

        // Add listeners to handle changes
        weaponTypeDropdowns[0].onValueChanged.AddListener(OnWeapon1TypeChanged);
        //weaponTypeDropdowns[1].onValueChanged.AddListener(OnWeapon2TypeChanged);
    }

    private void OnWeapon1TypeChanged(int index)
    {
        WeaponType selectedType = (WeaponType)index;
        WeaponManager.Instance.Initialized((int)selectedType);
    }

    private void OnWeapon2TypeChanged(int index)
    {
        WeaponType selectedType = (WeaponType)index;
    }
}
