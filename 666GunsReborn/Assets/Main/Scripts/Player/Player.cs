using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Player Enum
public enum PlayerStateType
{
    Idle,
    Move,
    Dash,
    Attack,
    Dead
}

public enum PlayerType
{
    Attack,
    Defense,
    Balance,
}
#endregion

public struct SlopeInfo
{
    public bool onSlope;
    public float angle;
    public Vector3 normal;
}

public class Player : MonoBehaviour, IPlayer
{
    #region Player 변수
    // 플레이어 상태 딕셔너리
    private Dictionary<PlayerStateType, PlayerStateBase> stateMap;
    // 플레이어 공격 시스템
    [NonSerialized]
    public PlayerAttack attackSystem;
    // 플레이어 상태(상태 패턴)
    private PlayerStateBase currentState;

    // 플레이어 스텟
    [NonSerialized]
    public PlayerStats stats;
    // 적 스캐너
    [NonSerialized]
    public EnemyScanner scanner;

    // 플레이어 컴포넌트
    [NonSerialized]
    public Rigidbody rigid;
    // 애니메이터
    [NonSerialized]
    public Animator anim;
    public RuntimeAnimatorController[] animationControllers;

    private bool hasPlayedWalkSound = false;
    #endregion

    #region Awake
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // 초기화 작업
        Debug.Log("Player Initialized");
        Initialized(PlayerType.Attack);
    }
    #endregion

    #region Player Initialization
    // 플레이어 스텟 초기화
    public virtual void Initialized(PlayerType playerType)
    {
        // 플레이어 타입에 따른 데이터 로드
        //PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");
        Debug.Log($"Player Type: {playerType}");

        if (!gameObject.TryGetComponent<PlayerStats>(out stats))
            stats = gameObject.AddComponent<PlayerStats>();
        if (!gameObject.TryGetComponent<EnemyScanner>(out scanner))
            scanner = gameObject.AddComponent<EnemyScanner>();

        InitStats(playerType);

        // 공격 시스템 초기화
        if (!gameObject.TryGetComponent<PlayerAttack>(out attackSystem))
            attackSystem = gameObject.AddComponent<PlayerAttack>();
        attackSystem.Initialize(this);
        // 이벤트 등록
        AddControllerEvent();

        // 상태 초기화
        stateMap = new Dictionary<PlayerStateType, PlayerStateBase>
        {
            { PlayerStateType.Idle, new IdleState(this) },
            { PlayerStateType.Move, new MoveState(this) },
            { PlayerStateType.Dash, new DashState(this) },
        };
        SetState(PlayerStateType.Idle);

        //애니메이션 초기화
        anim.applyRootMotion = false;
    }

    public void InitStats(PlayerType playerType)
    {
        PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/" + "FormOf" + playerType.ToString() + "Player");
        stats.Init(playerData);
    }

    private void AddControllerEvent()
    {
        // 행동에 대한 이벤트 등록
        PlayerActionEvent.OnMovePress += HandleInput;
        PlayerActionEvent.OnAttackPress += attackSystem.RequestAttack;
        PlayerActionEvent.OnAttackReleased += attackSystem.CancelAttackRequest;
        PlayerActionEvent.OnDashPress += Dash;

        Debug.Log("[Player] 이벤트 등록 완료");
    }

    private void OnDestroy()
    {
        // 이벤트 해체
        Debug.Log("[Player] 이벤트 해제 완료");
        PlayerActionEvent.OnMovePress -= HandleInput;
        PlayerActionEvent.OnAttackPress -= attackSystem.RequestAttack;
        PlayerActionEvent.OnAttackReleased -= attackSystem.CancelAttackRequest;
        PlayerActionEvent.OnDashPress -= Dash;

        stateMap.Clear();
    }
    #endregion

    #region Player Animation
    public void SwitchPlayerAnimation()
    {
        anim.runtimeAnimatorController = animationControllers[(int)Weapons.WeaponManager1.Instance.GetCurrentWeaponType()];
    }
    #endregion

    #region Player State Management
    //상태 설정
    public void SetState(PlayerStateType type)
    {
        currentState?.ExitState();
        currentState = stateMap[type];
        currentState?.EnterState();
    }

    protected virtual void Update()
    {
        // 현재 상태 업데이트
        currentState?.Update();

        // 대쉬 쿨타임 계산
        stats.DashCountCoolDown();
    }

    public void HandleInput(Vector3 direction)
    {
        if (currentState is DashState)
            return;
            
        currentState?.HandleInput(direction);
    }
    #endregion

    #region Player Dash
    public void Dash()
    {
        // 대쉬 상태일 경우 대쉬 불가
        if (currentState is DashState)
        {
            Debug.Log("Already dashing, cannot dash again.");
            return;
        }

        //대쉬 개수 부족할 경우 대쉬 불가
        if (stats.CurrentDashCount <= 0)
        {
            Debug.Log("Not enough dash count.");
            return;
        }

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        Debug.Log("Player Dash");
        // 대쉬 상태 변환
        SetState(PlayerStateType.Dash);

        float dashTime = stats.CurrentDashDistance / 50f;
        currentState?.HandleInput(gameObject.transform.forward);
        yield return new WaitForSeconds(dashTime);

        SetState(PlayerStateType.Idle);
        anim.SetBool("IsDash", false);
    }
    #endregion

    #region Attack Action
    public virtual void StartAttack()
    {
        Debug.Log("공격 애니메이션 시작");
        anim.SetBool("IsAttack", true);
        //attackSystem.StartAttack();
    }

    public virtual void StopAttack()
    {
        anim.SetBool("IsAttack", false);
        //attackSystem.StopAttack();
    }
    #endregion

    #region Player Hit
    public void Hit(int damage)
    {
        Debug.Log("Player Hit");
        bool isDead = stats.DecreaseHealth(damage);

        if (isDead)
            Dead();
    }

    private void Dead()
    {
        Debug.Log("Player Dead");
        StageManager.Instance.StageFailed();
    }
    #endregion
}