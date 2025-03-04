using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private Dictionary<ArmorType, Armor> equippedArmor = new Dictionary<ArmorType, Armor>();

    public void EquipArmor(Armor newArmor)
    {
        if (equippedArmor.ContainsKey(newArmor.armorType))
        {
            //선택된 타입의 방어구는 이미 착용됨
        }
        else
        {
            equippedArmor.Add(newArmor.armorType, newArmor);
        }
    }
}