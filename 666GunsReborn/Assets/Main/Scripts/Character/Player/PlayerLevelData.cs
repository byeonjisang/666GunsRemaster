using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerLevelData", menuName = "ScriptableObjects/PlayerLevelData", order = 1)]
public class PlayerLevelData : ScriptableObject
{
    // 레벨 당 증가하는 이동속도
    [SerializeField] private float moveSpeedIncreasePerLevel;
    public float MoveSpeedIncreasePerLevel => moveSpeedIncreasePerLevel;
    
    // 레벨 당 증가하는 대쉬 거리
    [SerializeField] private float dashCooldownIncreasePerLevel;
    public float DashCooldownIncreasePerLevel => dashCooldownIncreasePerLevel;

    // 레벨 당 증가하는 대쉬 거리
    [SerializeField] private float dashDistanceIncreasePerLevel;
    public float DashDistanceIncreasePerLevel => dashDistanceIncreasePerLevel;

    // 레벨 당 증가하는 체력
    [SerializeField] private float healthIncreasePerLevel;
    public float HealthIncreasePerLevel => healthIncreasePerLevel;
}