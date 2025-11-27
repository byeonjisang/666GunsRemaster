using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter
{
    // 데이터가 Player에게 있어 Model 대신 Channel을 참조
    private PlayerChannel _playerChannel;

    // View 참조
    private FireButtonView _fireButtonView;
    private DashButtonView _dashButtonView;
    private MoveJoystickView _moveJoystickView;
    private WeaponView _weaponView;
    
    // 생성자
    public PlayerPresenter(PlayerChannel playerChannel,
                           FireButtonView fireButtonView,
                           DashButtonView dashButtonView,
                           MoveJoystickView moveJoystickView,
                           WeaponView weaponView)
    {
        _playerChannel = playerChannel;

        _fireButtonView = fireButtonView;
        _dashButtonView = dashButtonView;
        _moveJoystickView = moveJoystickView;
        _weaponView = weaponView;

        // UI -> Player 이벤트 등록
        // 이동
        _moveJoystickView.OnMove += playerChannel.SendMoveCommand;
        // 대쉬
        _dashButtonView.OnClick += _playerChannel.SendDashCommand;
        // 공격
        _fireButtonView.OnFirePointerDown += _playerChannel.SendFirePointerDown;
        _fireButtonView.OnFirePointerUp += _playerChannel.SendFirePointerUp;
        // 무기 변경
        _weaponView.OnClick += _playerChannel.SendChangedWeaponCommand;

        // Player -> UI 이벤트 등록
        // 무기 변경
        _playerChannel.OnWeaponChanged += _weaponView.UpdateWeaponUI;
        // 무기 변경 쿨타임
        _playerChannel.OnChangedWeaponCooldown += _weaponView.UpdateWeaponChangeCooldown;
        // 무기 총알 UI 업데이트
        _playerChannel.OnUpdateBullet += _weaponView.UpdateWeaponBulletUI;
        // 무기 재장전 시간표시 UI 업데이트
        _playerChannel.OnReloadTime += _weaponView.UpdateWeaponReloadSlider;
    }

    // 씬 넘어갈 때 이벤트들 해체
    public void Dispose()
    {
        _moveJoystickView.OnMove -= _playerChannel.SendMoveCommand;
        _dashButtonView.OnClick -= _playerChannel.SendDashCommand;
        _fireButtonView.OnFirePointerDown -= _playerChannel.SendFirePointerDown;
        _fireButtonView.OnFirePointerUp -= _playerChannel.SendFirePointerUp;
        _weaponView.OnClick -= _playerChannel.SendChangedWeaponCommand;
    }
}
