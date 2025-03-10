using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : Singleton<ArmorManager>
{
    [SerializeField]
    private List<ArmorData> armorDatas = new List<ArmorData>();

    //전체 방어구 딕셔너리
    private Dictionary<string, ArmorData> armorInventory = new Dictionary<string, ArmorData>();

    private PlayerEquipment playerEquipment;

    private void Start()
    {
        foreach (ArmorData armorData in armorDatas)
        {
            armorInventory.Add(armorData.armorName, armorData);
        }
    }

    public void EquipArmor(string armorName)
    {
        ArmorData armorData = null;

        if(armorInventory.TryGetValue(armorName, out armorData))
        {
            playerEquipment.EquipArmor(armorData);
        }
        else
        {
            //해당 타입의 방어구 리스트가 없을 경우
            Debug.LogError($"{armorName}은 존재하지 않는 방어구 입니다.");
        }
    }
}
