using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : Singleton<ArmorManager>
{
    [SerializeField]
    private List<ArmorData> armorDatas = new List<ArmorData>();

    //��ü �� ��ųʸ�
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
            //�ش� Ÿ���� �� ����Ʈ�� ���� ���
            Debug.LogError($"{armorName}�� �������� �ʴ� �� �Դϴ�.");
        }
    }
}
