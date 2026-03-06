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
    // 무기 이미지 초기화
    public Action<int, Sprite> OnWeaponSprite;
    public void SendWeaponSprite(int weaponIndex, Sprite weaponSprite)
    {
        OnWeaponSprite?.Invoke(weaponIndex, weaponSprite);  
    } 

    // 무기 변경
    public Action<int> OnWeaponChanged;
    public void SendWeaponChanged(int weaponIndex) => OnWeaponChanged?.Invoke(weaponIndex);
    
    // 무기 변경 쿨타임
    public Action<float> OnChangedWeaponCooldown;
    public void SendChangedWeaponCooldown(float cooldownTime) => OnChangedWeaponCooldown?.Invoke(cooldownTime);

    // 무기 발사
    public Action<int, int, int> OnUpdateBullet;
    // maxMagazine: 최대 탄알, currentMagazine: 현재 탄알, index: 무기 인덱스(0: 주무기, 1: 보조무기)
    // 초기화 시 index 값 지정, 그 외 재장정 및 발사 시 index 값은 기본 0
    public void SendUpdateBullet(int maxMagazine, int currentMagazine, int index = 0) => OnUpdateBullet?.Invoke(maxMagazine, currentMagazine, index);  

    // 무기 재장전 시간표시
    public Action<float, float> OnReloadTime;
    public void SendReloadTime(float maxReloadTime, float currentReloadTime) => OnReloadTime?.Invoke(maxReloadTime, currentReloadTime);

    #endregion
}
