using System;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.Player
{
    public struct SlopeInfo
    {
        public bool onSlope;
        public float angle;
        public Vector3 normal;
    }

    public class Player : Character
    {
        [Header("Player Data")]
        [SerializeField] private PlayerData playerData;
        [Header("Player Type Data")]
        [SerializeField] private PlayerTypeData playerTypeData;

        // 플레이어 상태변환 컨텍스트
        public PlayerStateContext StateContext { get; private set;}
        // 플레이어 스텟
        public PlayerStats Stat { get; private set; }

        // 플레이어 정지 시스템
        public PlayerIdle IdleSystem { get; private set; }
        // 플레이어 이동 시스템
        public PlayerMove MoveSystem { get; private set; }
        // 플레이어 대쉬 시스템
        public PlayerDash DashSystem { get; private set; }
        // 플레이어 공격 시스템
        public PlayerAttack AttackSystem { get; private set; }

        // 적 스캐너
        public EnemyScanner Scanner { get; private set; }
        // 플레이어 채널( UI 및 입력 이벤트 )
        [Header("Player Channel")]
        [SerializeField] private PlayerChannel playerChannel;

        // 무기 매니저
        public Weapon.WeaponManager WeaponManager { get; private set; }

        // 입력 방향
        public Vector3 InputDirection { get; private set; }
        
        private void Start()
        {
            // 플레이어 초기화
            Init();
        }

        #region Initialize
        /// <summary>
        /// Player 초기화 메서드
        /// </summary>
        /// <param name="playerType"></param>
        public void Init()
        {
            // 플레이어 필요 컴포넌트 추가
            WeaponManager = new Weapon.WeaponManager(playerChannel);
            // 무기 매니저 임시 초기화
            // TODO: 나중에 무기 선택하는 기능 따로 구현해야함
            WeaponManager.Init(Weapon.WeaponID.Pistol_Slide, Weapon.WeaponID.Revolver);
            StateContext = new PlayerStateContext(this);
            Stat = new PlayerStats();
            Scanner = new EnemyScanner(this);
            AttackSystem = new PlayerAttack(this);
            IdleSystem = new PlayerIdle(this);
            MoveSystem = new PlayerMove(this);
            DashSystem = new PlayerDash(this);

            // 플레이어 타입에 따른 스텟 초기화
            // TODO:: Resources이 성능이 좋지 않아 변경해야 함
            Stat.Init(playerData, playerTypeData);

            // 이벤트 등록
            AddEvent();

            // 초기 상태 설정
            StateContext.TransitionTo(StateContext.IdleState);

            // 애니메이션으로 인한 이동 방지
            Anim.applyRootMotion = false;
        }

        // 이벤트 등록
        private void AddEvent()
        {
            // 입력 이벤트 등록
            playerChannel.OnMoveCommand += HandleInput;
            playerChannel.OnFirePointerDown += AttackSystem.RequestAttack;
            playerChannel.OnFirePointerUp += AttackSystem.CancelAttackRequest;
            playerChannel.OnDashCommand += OnDash;
            playerChannel.OnChangedWeaponCommand += WeaponManager.SwitchWeapon;
        }

        // Player 사라지면 실행되는 메서드
        private void OnDestroy()
        {
            // 이벤트 해체
            playerChannel.OnMoveCommand -= HandleInput;
            playerChannel.OnFirePointerDown -= AttackSystem.RequestAttack;
            playerChannel.OnFirePointerUp -= AttackSystem.CancelAttackRequest;
            playerChannel.OnDashCommand -= OnDash;
            playerChannel.OnChangedWeaponCommand -= WeaponManager.SwitchWeapon;
        }
        #endregion

        #region Update
        private void Update()
        {
            // 상태 업데이트
            StateContext.CurrentState.UpdateState();
            // 대쉬 쿨타임 업데이트
            Stat.DashCooldownUpdate();
            // 적 스캐너 업데이트
            Scanner.ScannerUpdate();
            // WeaponManager 업데이트
            WeaponManager.Update();
        }
        #endregion

        #region Player Dash
        // 대쉬 입력 처리 메서드
        private void OnDash()
        {
            // 대쉬 상태일 때는 무시
            if(StateContext.CurrentState is DashState)
                return;
            // TODO: 사망했을 때 대쉬 무시 처리 필요

            // 대쉬 가능 여부 체크
            if(Stat.CurrentDashCount <= 0)
                return;
            
            // 대쉬 실행
            StateContext.TransitionTo(StateContext.DashState);
        }
        #endregion

        #region Player Input
        // 이동 입력 처리 메서드
        public void HandleInput(Vector3 direction)
        {
            InputDirection = direction;
        }
        #endregion

        #region Player Hit
        public override void TakeDamage(float damage)
        {
            Debug.Log("Player Hit");
            bool isDead = Stat.TakeDamage(damage);

            if (isDead)
                Dead();
        }

        protected override void Dead()
        {
            Debug.Log("Player Dead");
            StageManager.Instance.StageFailed();
        }
        #endregion
    }
}