using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    //장비 관리 스크립트
    private WeaponManager weaponManager;
    private ArmorManager armorManager;

    private void Start()
    {
        weaponManager = gameObject.AddComponent<WeaponManager>();
        armorManager = gameObject.AddComponent<ArmorManager>();

        armorManager.Init();
    }

    #region Weapon
    public void EquipWeapon(WeaponData weaponData)
    {

    }

    public void UnEquipWeapon()
    {

    }
    #endregion

    #region Armor
    public void EquipArmor(ArmorType armorType, string armorName)
    {
        //armorManager.Init(armorType, armorName);
        armorManager.Equip(armorType, armorName);
    }

    public void unEquipArmor(ArmorType armorType, string armorName)
    {
        armorManager.UnEquip(armorType, armorName);
    }
    #endregion
}