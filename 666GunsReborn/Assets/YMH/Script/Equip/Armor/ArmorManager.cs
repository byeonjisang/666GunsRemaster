using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    //���� ���� ����
    private List<Armor> equipArmor = new List<Armor>();

    public void Init()
    {
        equipArmor.Clear();
        equipArmor = new List<Armor>();

        int ArmorTypeLength = System.Enum.GetValues(typeof(ArmorType)).Length;
        for(int i = 0; i < ArmorTypeLength; i++)
        {
            equipArmor.Add(null);
        }
    }

    public void Equip(ArmorType armorType, string armorName)
    {
        //�� ������ ���� �ҷ�����
        ArmorData armorData = null;
        switch (armorType)
        {
            case ArmorType.Head:
                armorData = Resources.Load<ArmorData>($"Datas/Armor/Head/{armorName}");
                break;
            case ArmorType.Body:
                armorData = Resources.Load<ArmorData>($"Datas/Armor/Body/{armorName}");
                break;
            case ArmorType.Leg:
                armorData = Resources.Load<ArmorData>($"Datas/Armor/Leg/{armorName}");
                break;
        }

        //�� ������ �ʱ�ȭ �� ����
        Armor armor = new Armor(armorData);
        equipArmor[(int)armorType] = armor;

        Debug.Log($"��� ���� : ArmorType : {armorType}, armorName : {armorName}, List �� ��� : {equipArmor[(int)armorType]}");
    }

    public void UnEquip(ArmorType armorType, string armorName)
    {
        equipArmor[(int)armorType] = null;
    }
}
