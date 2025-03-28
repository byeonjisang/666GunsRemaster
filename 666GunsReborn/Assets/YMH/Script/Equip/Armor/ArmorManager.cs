using System.Collections.Generic;
using UnityEngine;
using static ArmorManager;

public class ArmorManager : Singleton<ArmorManager>
{
    //���� ���� ����
    public List<Armor> EquipArmor = new List<Armor>();

    //�̺�Ʈ
    public delegate void OnEquipArmor();
    public event OnEquipArmor onEquipArmor;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        EquipArmor.Clear();
        EquipArmor = new List<Armor>();

        int ArmorTypeLength = System.Enum.GetValues(typeof(ArmorType)).Length;
        for(int i = 0; i < ArmorTypeLength; i++)
        {
            EquipArmor.Add(null);
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
        EquipArmor[(int)armorType] = armor;

        onEquipArmor?.Invoke();

        Debug.Log($"��� ���� : ArmorType : {armorType}, armorName : {armorName}, List �� ��� : {EquipArmor[(int)armorType]}");
    }

    public void UnEquip(ArmorType armorType, string armorName)
    {
        EquipArmor[(int)armorType] = null;
    }
}
