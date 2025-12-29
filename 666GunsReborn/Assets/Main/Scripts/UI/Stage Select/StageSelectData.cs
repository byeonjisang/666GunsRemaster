using UnityEngine;

[CreateAssetMenu(fileName = "StageSelectData", menuName = "ScriptableObjects/StageSelectData", order = 1)]
public class StageSelectData : ScriptableObject
{
    // 선택한 스테이지
    public StageData StageData;
    // 선택한 무기
    public Weapon.WeaponID[] SelectedWeaponsID = new Weapon.WeaponID[2];
}