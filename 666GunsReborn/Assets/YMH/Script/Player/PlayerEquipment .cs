using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment
{
    private Dictionary<ArmorType, Armor> equippedArmor = new Dictionary<ArmorType, Armor>();

    //방어구 착용
    public void EquipArmor(Armor newArmor)
    {
        if (equippedArmor.ContainsKey(newArmor.ArmorType))
        {
            //선택된 타입의 방어구는 이미 착용됨
        }
        else
        {
            //방어구 착용
            equippedArmor.Add(newArmor.ArmorType, newArmor);
        }
    }
}