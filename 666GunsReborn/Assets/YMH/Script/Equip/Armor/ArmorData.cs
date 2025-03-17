using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    Head,
    Body,
    Leg,
}


[CreateAssetMenu(fileName = "ArmorData", menuName = "Datas/ArmorData", order = 2)]
public class ArmorData : ScriptableObject
{
    public string armorName;
    public ArmorType armorType;
    public int armorValue;
    public int speedValue;
    public int healthValue;
}
