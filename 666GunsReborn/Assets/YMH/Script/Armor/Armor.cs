using System.Collections;
using UnityEngine;

public class Armor
{
    //방어구 공통 변수
    protected ArmorData armorData;
    protected ArmorType armorType;

    //공개 변수
    public ArmorType ArmorType { get { return armorType; } }
}