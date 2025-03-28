using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//장비 착용 테스트 GUI
public class ArmorGUI : MonoBehaviour
{
    [SerializeField]
    //private PlayerEquipment playerEquipment;

    private Dropdown[] dropdowns;

    private void Start()
    {
        dropdowns = GetComponentsInChildren<Dropdown>();

        dropdowns[0].onValueChanged.AddListener(EquipHeadArmor);
        dropdowns[1].onValueChanged.AddListener(EquipBodyArmor);
        dropdowns[2].onValueChanged.AddListener(EquipLegArmor);
    }

    public void EquipHeadArmor(int index)
    {
        Debug.Log("Equip Head Armor: " + index);
        ArmorManager.Instance.Equip(ArmorType.Head, "Head" + (index + 1));
        //EquipManager.Instance.EquipArmor(ArmorType.Head, "Head" + (index + 1));
    }

    public void EquipBodyArmor(int index)
    {
        Debug.Log("Equip Body Armor: " + index);
        ArmorManager.Instance.Equip(ArmorType.Body, "Body" + (index + 1));
        //EquipManager.Instance.EquipArmor(ArmorType.Body, "Body" + (index + 1));
    }

    public void EquipLegArmor(int index)
    {
        Debug.Log("Equip Leg Armor: " + index);
        ArmorManager.Instance.Equip(ArmorType.Leg, "Leg" + (index + 1));
        //EquipManager.Instance.EquipArmor(ArmorType.Leg, "Leg" + (index + 1));
    }
}