using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : Singleton<ArmorManager>
{
    //private Dictionary<ArmorType, List<Armor>> armorInventory = new Dictionary<ArmorType, List<Armor>>();
    [SerializeField]
    private SerializableDictionary<ArmorType, List<Armor>> armorInventory = new SerializableDictionary<ArmorType, List<Armor>>();

    private PlayerEquipment playerEquipment;

    public void EquipArmor(ArmorType type, int armorIndex)
    {
        List<Armor> armors = null;

        if(armorInventory.TryGetValue(type, out armors))
        {
            playerEquipment.EquipArmor(armors[armorIndex]);
        }
        else
        {
            //해당 타입의 방어구 리스트가 없을 경우
            Debug.LogError("No armor list for " + type);
        }
    }
}
