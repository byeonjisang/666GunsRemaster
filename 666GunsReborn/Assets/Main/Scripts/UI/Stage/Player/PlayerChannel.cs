using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerChannel", menuName = "ScriptableObjects/Channels/PlayerChannel")]
public class PlayerChannel : ScriptableObject
{
    #region UI Button -> Player
    // 대쉬 명령 이벤트
    public event Action OnDashCommand;

    public void SendDashCommand() => OnDashCommand?.Invoke();

    // 공격
    public event Action OnFirePointerDown;
    public event Action OnFirePointerUp;
    public void SendFirePointerDown() => OnFirePointerDown?.Invoke();
    public void SendFirePointerUp() => OnFirePointerUp?.Invoke();

    // 이동
    public event Action<Vector3> OnMoveCommand;
    public void SendMoveCommand(Vector3 direction) => OnMoveCommand?.Invoke(direction);

    // 무기
    public event Action OnChangedWeaponCommand;
    public void SendChangedWeaponCommand() => OnChangedWeaponCommand?.Invoke();
    #endregion
    
    #region Player -> UI
    // 무기 변경
    public event Action<int> OnWeaponChanged;
    public void SendWeaponChanged(int weaponIndex) => OnWeaponChanged?.Invoke(weaponIndex);
    
    // 무기 변경 쿨타임
    public event Action<float> OnChangedWeaponCooldown;
    public void SendChangedWeaponCooldown(float cooldownTime) => OnChangedWeaponCooldown?.Invoke(cooldownTime);

    // 무기 발사
    public event Action<int, int> OnUpdateBullet;
    public void SendUpdateBullet(int maxMagazine, int currentMagazine) => OnUpdateBullet?.Invoke(maxMagazine, currentMagazine);

    // 무기 재장전 시간표시
    public event Action<float, float> OnReloadTime;
    public void SendReloadTime(float maxReloadTime, float currentReloadTime) => OnReloadTime?.Invoke(maxReloadTime, currentReloadTime);

    #endregion
}
