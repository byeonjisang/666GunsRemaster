using System.Collections.Generic;
using UnityEngine;
using static ArmorManager;

public class ArmorManager : Singleton<ArmorManager>
{
    //착용 중인 상태
    public List<Armor> EquipArmor = new List<Armor>();

    //이벤트
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
        //방어구 데이터 파일 불러오기
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

        //방어구 데이터 초기화 및 착용
        Armor armor = new Armor(armorData);
        EquipArmor[(int)armorType] = armor;

        onEquipArmor?.Invoke();

        Debug.Log($"장비 착용 : ArmorType : {armorType}, armorName : {armorName}, List 안 장비 : {EquipArmor[(int)armorType]}");
    }

    public void UnEquip(ArmorType armorType, string armorName)
    {
        EquipArmor[(int)armorType] = null;
    }
}
