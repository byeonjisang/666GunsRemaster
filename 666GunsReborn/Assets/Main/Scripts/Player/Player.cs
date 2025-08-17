using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
    // 플레이어 공격 Aciton
    public PlayerAttack attackSystem;
    // 플레이어 상태(상태 패턴)
    private PlayerStateBase currentState;

    // 플레이어 스텟
    public PlayerStats stats;
    // 적 스캐너
    public EnemyScanner scanner;

    // 플레이어 컴포넌트
    public Rigidbody rigid;
    // 애니메이터
    public Animator anim;
    protected RigController rigController;

    private bool hasPlayedWalkSound = false;
    #endregion

    #region Awake
    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    #endregion

    #region Player Initialization
    // 플레이어 스텟 초기화
    public virtual void Initialized(Type playerType)
    {
        // 플레이어 타입에 따른 데이터 로드
        //PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");
        Debug.Log($"Player Type: {playerType}");
        PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/" + playerType.ToString());

        if(!gameObject.TryGetComponent<PlayerStats>(out stats))
            stats = gameObject.AddComponent<PlayerStats>();
        if(!gameObject.TryGetComponent<EnemyScanner>(out scanner))
            scanner = gameObject.AddComponent<EnemyScanner>();

        // 스텟 초기화
        stats.Init(playerData);

        // 상태 초기화
        stateMap = new Dictionary<PlayerStateType, PlayerStateBase>
        {
            { PlayerStateType.Idle, new IdleState(this) },
            { PlayerStateType.Move, new MoveState(this) },
            { PlayerStateType.Dash, new DashState(this) },
        };
        SetState(PlayerStateType.Idle);

        // 공격 시스템 초기화
        if (!gameObject.TryGetComponent<PlayerAttack>(out attackSystem))
            attackSystem = gameObject.AddComponent<PlayerAttack>();
        attackSystem.Initialize(this);
        AddControllerEvent();

        //애니메이션 초기화
        anim.applyRootMotion = false;
        rigController = GetComponent<RigController>();
    }

    private void AddControllerEvent()
    {
        // Add event listeners in the PlayerController
        PlayerActionEvent.OnMovePress += HandleInput;
        PlayerActionEvent.OnAttackPress += attackSystem.RequestAttack;
        PlayerActionEvent.OnAttackReleased += attackSystem.CancelAttackRequest;
        PlayerActionEvent.OnDashPress += Dash;
    }

    private void OnDisable()
    {
        PlayerActionEvent.OnMovePress -= HandleInput;
        PlayerActionEvent.OnAttackPress -= attackSystem.RequestAttack;
        PlayerActionEvent.OnAttackReleased -= attackSystem.CancelAttackRequest;
        PlayerActionEvent.OnDashPress -= Dash;
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
        currentState?.HandleInput(direction);
    }
    #endregion

    #region Player Dash
    public void Dash(Vector3 direction)
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
            

        DashCoroutine(direction);
    }

    private IEnumerator DashCoroutine(Vector3 direction)
    {
        Debug.Log("Player Dash");
        // 대쉬 상태 변환
        SetState(PlayerStateType.Dash);

        float dashTime = stats.CurrentDashDistance / 10f;
        yield return new WaitForSeconds(dashTime);

        SetState(PlayerStateType.Idle);
        anim.SetBool("IsDash", false);
    }
    #endregion

    #region Attack Action
    public virtual void StartAttack()
    {
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