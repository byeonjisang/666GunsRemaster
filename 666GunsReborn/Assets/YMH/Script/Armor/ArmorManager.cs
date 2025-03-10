using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : Singleton<ArmorManager>
{
    private Dictionary<ArmorType, List<Armor>> armorInventory = new Dictionary<ArmorType, List<Armor>>();

    public void EquipArmor(ArmorType type, int armorIndex)
    {
        List<Armor> armors = null;

        if(armorInventory.TryGetValue(type, out armors))
        {

        }
        else
        {
            //�ش� Ÿ���� �� ����Ʈ�� ���� ���
            Debug.LogError("No armor list for " + type);
        }
    }
}
