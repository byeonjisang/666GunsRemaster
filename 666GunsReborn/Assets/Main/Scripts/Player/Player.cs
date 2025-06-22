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
    #region Player State
    // 플레이어 상태 딕셔너리
    private Dictionary<PlayerStateType, PlayerStateBase> stateMap;
    // 플레이어 공격 Aciton
    private PlayerAttack attackSystem;
    // 플레이어 상태(상태 패턴)
    private PlayerStateBase currentState;
    #endregion Player State

    // 플레이어 스텟
    protected PlayerStats stats;
    // 적 스캐너
    protected EnemyScanner scanner;

    // 플레이어 컴포넌트
    protected Rigidbody rigid;
    // 애니메이터
    protected Animator anim;
    protected RigController rigController;

    protected PlayerController controller;

    private bool hasPlayedWalkSound = false;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        stats = gameObject.AddComponent<PlayerStats>();
        scanner = gameObject.AddComponent<EnemyScanner>();
    }

    // 플레이어 스텟 초기화
    public virtual void Initialized(Type playerType)
    {
        // 플레이어 타입에 따른 데이터 로드
        //PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");
        PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/Player1");

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
        attackSystem = gameObject.AddComponent<PlayerAttack>();
        attackSystem.Initialize(this);
        AddControllerEvent();

        //애니메이션 초기화
        anim.applyRootMotion = false;
        rigController = GetComponent<RigController>();
    }

    private void AddControllerEvent()
    {
        // Get ther PlayerController component
        controller = GetComponent<PlayerController>();
        if (controller == null)
        {
            Debug.LogError("PlayerController component not found on Player GameObject.");
            return;
        }

        // Add event listeners in the PlayerController
        controller.OnMovePress += HandleInput;
        controller.OnAttackPress += attackSystem.RequestAttack;
        controller.OnAttackReleased += attackSystem.CancelAttackRequest;
        controller.OnDashPress += Dash;
    }

    //상태 설정
    public void SetState(PlayerStateType type)
    {
        currentState?.ExitState();
        currentState = stateMap[type];
        currentState.EnterState();
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

    private void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            if (scanner.NearestEnemy != null && attackSystem.IsAttacking)
            {
                Debug.Log("플레이어가 적을 스캔함");

                //적의 위치로 회전
                direction = scanner.NearestEnemy.transform.position - transform.position;
                direction.y = 0f;
            }

            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rigid.rotation = targetAngle;
        }
    }

    #region Player Idle
    public void Idle(Vector3 direction)
    {
        var slopeInfo = GetSlopeInfo(direction);

        if (slopeInfo.onSlope && IsGrounded())
            rigid.velocity = Vector3.zero;
    }
    #endregion

    #region Player Move
    public void MoveDirectly(Vector3 direction)
    {
        var slopeInfo = GetSlopeInfo(direction);

        // 경사면 가파른 경우 못올라감
        if (slopeInfo.angle > 45f && IsGrounded())
        {
            Debug.Log("경사면 너무 가파름");
            return;
        }

        LookAt(direction);

        // 경사면 속도 보정
        if (slopeInfo.onSlope && IsGrounded())
        {
            Vector3 slopeDir = Vector3.ProjectOnPlane(direction, slopeInfo.normal);
            rigid.velocity = slopeDir * stats.CurrentMoveSpeed;
        }
        else
        {
            //평소 땅 위
            Vector3 moveVelocity = new Vector3(direction.x, 0f, direction.z) * stats.CurrentMoveSpeed;
            rigid.velocity = new Vector3(moveVelocity.x, rigid.velocity.y, moveVelocity.z);
        }

        anim.SetFloat("Speed", direction.magnitude);


        // 조건 추가: 일정 이상 이동할 때만 재생, 재생한 적 없을 때만
        if (direction.magnitude > 0.1f && !hasPlayedWalkSound)
        {
            SoundManager.Instance.PlaySound(0);
            hasPlayedWalkSound = true;
        }
        else if (direction.magnitude <= 0.1f)
        {
            hasPlayedWalkSound = false;
        }
    }

    // 땅 위에 있는지 체크
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // 플레이어 이동시 경사면 보정
    private SlopeInfo GetSlopeInfo(Vector3 direction)
    {
        RaycastHit hit;
        SlopeInfo info = new SlopeInfo
        {
            onSlope = false,
            angle = 0f,
            normal = Vector3.up
        };

        Vector3 rayDir = Vector3.down + direction.normalized * 0.5f;

        if (Physics.Raycast(transform.position, rayDir.normalized, out hit, 1.5f))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            info.onSlope = angle > 0 && angle < 45f;
            info.angle = angle;
            info.normal = hit.normal;
        }

        return info;
    }
    #endregion

    #region Player Dash
    public void Dash(Vector3 direction)
    {
        // 대쉬 상태일 경우 대쉬 불가
        if (currentState is DashState)
            return;

        //대쉬 개수 부족할 경우 대쉬 불가
        if (stats.CurrentDashCount <= 0)
            return;

        StartCoroutine(DashCoroutine(direction));
    }

    private IEnumerator DashCoroutine(Vector3 direction)
    {
        Debug.Log("Player Dash");
        // 대쉬 상태 변환
        SetState(PlayerStateType.Dash);
        // 대쉬 개수 감소
        stats.CurrentDashCount--;
        // 애니메이션
        anim.SetBool("IsDash", true);

        // 이동을 하고 있지 않다면 바라보는 방향으로 대쉬
        if (direction.sqrMagnitude < 0.1f)
            direction = transform.forward.normalized;
        // 대쉬 이동
        rigid.velocity = direction * stats.CurrentDashDistance;

        yield return new WaitForSeconds(0.5f);

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