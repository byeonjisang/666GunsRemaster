using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private PlayerChannel _playerChannel;

    [Header("Views")]
    [SerializeField] private FireButtonView _fireButtonView;
    [SerializeField] private DashButtonView _dashButtonView;
    [SerializeField] private MoveJoystickView _moveJoystickView;
    [SerializeField] private WeaponView _weaponView;

    // presenter
    //private PlayerPresenter _playerPresenter;

    private void Start()
    {
        _fireButtonView.Init();
        _dashButtonView.Init();
        _moveJoystickView.Init();
        _weaponView.Init();

        // 이벤트 연결
        SubscribeEvents();
    }

    // 이벤트 구독
    private void SubscribeEvents()
    {
        // UI -> Player 이벤트 등록
        // 이동 UI 이벤트 등록
        _moveJoystickView.Presenter.OnMove += _playerChannel.SendMoveCommand;
        // 대쉬 UI 이벤트 등록
        _dashButtonView.Presenter.OnClick += _playerChannel.SendDashCommand;
        // 공격 UI 이벤트 등록
        _fireButtonView.Presenter.OnFirePointerDown += _playerChannel.SendFirePointerDown;
        _fireButtonView.Presenter.OnFirePointerUp += _playerChannel.SendFirePointerUp;
        // 무기 변경 UI 이벤트 등록
        _weaponView.Presenter.OnClick += _playerChannel.SendChangedWeaponCommand;

        // Player -> UI 이벤트 등록
        // 초기 무기 Sprite 변경 이벤트
        _playerChannel.OnWeaponSprite += _weaponView.UpdateWeaponSprite;
        // 무기 변경 UI 업데이트 이벤트 등록
        _playerChannel.OnWeaponChanged += _weaponView.SwitchWeaponUI;
        // 무기 변경 쿨타임 UI 업데이트 이벤트 등록
        _playerChannel.OnChangedWeaponCooldown += _weaponView.UpdateWeaponChangeCooldown;
        // 무기 총알 UI 업데이트 이벤트 등록
        _playerChannel.OnUpdateBullet += _weaponView.UpdateWeaponBulletUI;
        // 무기 재장전 시간표시 UI 업데이트 이벤트 등록
        _playerChannel.OnReloadTime += _weaponView.UpdateWeaponReloadSlider;
    }

    // 이벤트 구독 해체
    private void OnDestroy()
    {
        // UI -> Player 이벤트 해체
        _moveJoystickView.Presenter.OnMove = null;
        _dashButtonView.Presenter.OnClick = null;
        _fireButtonView.Presenter.OnFirePointerDown = null;
        _fireButtonView.Presenter.OnFirePointerUp = null;
        _weaponView.Presenter.OnClick = null;

        // Player -> UI 이벤트 해체
        _playerChannel.OnWeaponChanged = null;
        _playerChannel.OnChangedWeaponCooldown = null;
        _playerChannel.OnUpdateBullet = null;
        _playerChannel.OnReloadTime = null;
    }
}
