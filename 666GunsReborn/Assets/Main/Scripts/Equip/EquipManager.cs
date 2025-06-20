using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 무기 연습장에서 DropDown을 통한 총기변경 시스템
/// </summary>
public class EquipManager : MonoBehaviour
{
    [Header("Weapon Dropdowns")]
    [SerializeField] private Dropdown slot0Dropdown;
    [SerializeField] private Dropdown slot1Dropdown;

    [SerializeField] private WeaponManager weaponManager;

    //이벤트 등록 및 초기화
    private void Start()
    {
        //InitDropdown(slot0Dropdown);
        //InitDropdown(slot1Dropdown);

        //slot0Dropdown.onValueChanged.AddListener(index => OnWeaponSelected(0, index));
        //slot1Dropdown.onValueChanged.AddListener(index => OnWeaponSelected(1, index));

        // 강제로 초기화 호출 (실시간 테스트 가능)
        //OnWeaponSelected(0, slot0Dropdown.value);
        //OnWeaponSelected(1, slot1Dropdown.value);
    }

    private void InitDropdown(Dropdown dropdown)
    {
        dropdown.ClearOptions();
        List<string> weaponNames = new List<string>
        {
            "Pistol",
            "Rifle",
            "Shotgun"
        };

        dropdown.AddOptions(weaponNames);
    }

    private void OnWeaponSelected(int slotIndex, int weaponIndex)
    {
        WeaponType selectedWeapon = (WeaponType)weaponIndex;
        weaponManager.ReplaceWeapon(slotIndex, selectedWeapon);
    }
}
