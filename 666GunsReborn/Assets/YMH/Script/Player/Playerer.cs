using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;



public class Playerer : MonoBehaviour
{
    // 플레이어 상태
    protected PlayerStateType state;
    // 플레이어 스텟
    protected PlayerStats stats;
    // 플레이어 스캐너
    protected EnemyScanner scanner;

    // 플레이어 컴포넌트
    protected Rigidbody rigid;
    protected Animator anim;

    // 플레이어 게임 중 획득 내용
    protected int coin = 0;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        stats = gameObject.AddComponent<PlayerStats>();
        scanner = gameObject.AddComponent<EnemyScanner>();
    }

    public virtual void Init()
    {
        PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");
        
        //스텟 초기화
        stats.Init(playerData);
        state = PlayerStateType.Idle;
    }

    protected virtual void Update()
    {
        //대쉬 쿨타임 계산
        stats.DashCountCoolDown();
    }

    //플레이어 회전
    private void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            if (scanner.NearestEnemy != null && state == PlayerStateType.Attack)
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

    #region Player Move
    public virtual void Move(Vector3 direction)
    {
        if (state == PlayerStateType.Dash)
            return;

        var slopeInfo = GetSlopeInfo(direction);

        if (direction.sqrMagnitude < 0.001f)
        {
            if (slopeInfo.onSlope && IsGrounded())
                rigid.velocity = Vector3.zero;

            state = PlayerStateType.Idle;

            Debug.Log("정지");
        }
        else
        {
            Debug.Log("이동");
            if (slopeInfo.angle > 45f && IsGrounded())
            {
                Debug.Log("경사면 너무 가파름");
                return;
            }

            LookAt(direction);

            if (slopeInfo.onSlope && IsGrounded())
            {
                Vector3 slopeDir = Vector3.ProjectOnPlane(direction, slopeInfo.normal);
                rigid.velocity = slopeDir * stats.CurrentMoveSpeed;
            }
            else
            {
                Vector3 moveVelocity = new Vector3(direction.x, 0f, direction.z) * stats.CurrentMoveSpeed;
                rigid.velocity = new Vector3(moveVelocity.x, rigid.velocity.y, moveVelocity.z);
            }
        }

        anim.SetFloat("Speed", direction.magnitude);
    }

    //땅 위에 있는지 체크
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    //플레이어 이동시 경사면 보정
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

    #region Player Attack
    public virtual void StartAttack()
    {
        state = PlayerStateType.Attack;
        anim.SetBool("IsAttack", true);
    }

    public virtual void StopAttack()
    {
        state = PlayerStateType.Idle;
        anim.SetBool("IsAttack", false);
    }
    #endregion

    #region Player Dash
    public virtual void Dash()
    {
        //대쉬 상태에서 대쉬 안됨
        if (state == PlayerStateType.Dash)
            return;

        //대쉬 개수 확인
        if (stats.CurrentDashCount <= 0)
            return;

        Debug.Log("대쉬");

        StartCoroutine(DashCoroutine());
    }

    protected virtual IEnumerator DashCoroutine()
    {
        //상태 변경
        state = PlayerStateType.Dash;
        stats.CurrentDashCount--;
        anim.SetBool("IsDash", true);

        //대쉬 이동
        Vector3 direction = transform.forward.normalized;
        rigid.velocity = direction * stats.CurrentDashDistance * 2;

        //대쉬 시간(하드 코딩 나중에 재구현 해야함)
        yield return new WaitForSeconds(0.5f);

        //대쉬 종료
        state = PlayerStateType.Idle;
        anim.SetBool("IsDash", false);
    }
    #endregion

    // 물리 충돌 처리
    protected virtual void OnTriggerEnter(Collider other)
    {
        //코인 획득
        if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            this.coin += coin.GetAmount();
            Destroy(other.gameObject);
        }
    }
}