using System.Collections;
using UnityEngine;

public class Armor
{
    //방어구 공통 변수
    //protected ArmorData armorData;
    protected ArmorType armorType;
    protected string armorName;

    public int ArmorValue { get; protected set; }
    public int SpeedValue { get; protected set; }
    public int HealthValue { get; protected set; }

    //공개 변수
    public ArmorType ArmorType { get { return armorType; } }

    public Armor() { }

    public Armor(ArmorData armorData)
    {
        ArmorData data = armorData;
        armorType = data.armorType;

        ArmorValue = data.armorValue;
        SpeedValue = data.speedValue;
        HealthValue = data.healthValue;
    }
}