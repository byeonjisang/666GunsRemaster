using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

#region Player Enum
public enum PlayerState 
{
    Idle,
    Move,
    Dash,
    Attack,
    Dead
}

public enum PlayerType 
{
    Player1,
    Player2
}
#endregion

public struct SlopeInfo
{
    public bool onSlope;
    public float angle;
    public Vector3 normal;
}

public class Player : MonoBehaviour
{
    // 플레이어 상태
    protected PlayerState state;
    // 플레이어 스텟
    protected PlayerStats stats;

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
    }

    public virtual void Init()
    {
        PlayerData playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");
        
        //스텟 초기화
        stats.Init(playerData);
        state = PlayerState.Idle;
    }

    protected virtual void Update()
    {
        //대쉬 쿨타임 계산
        stats.DashCountCoolDown();
    }

    #region Player Move
    public virtual void Move(Vector3 direction)
    {
        Debug.Log(rigid.velocity.y);
        if (state == PlayerState.Dash)
            return;

        var slopeInfo = GetSlopeInfo(direction);

        if (direction.sqrMagnitude < 0.001f)
        {
            if (slopeInfo.onSlope && IsGrounded())
                rigid.velocity = Vector3.zero;

            state = PlayerState.Idle;

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

    //플레이어 회전
    private void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rigid.rotation = targetAngle;
        }
    }
    #endregion

    #region Player Attack
    public virtual void Attack()
    {
        anim.SetTrigger("Attack");
    }
    #endregion

    #region Player Dash
    public virtual void Dash()
    {
        //대쉬 상태에서 대쉬 안됨
        if (state == PlayerState.Dash)
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
        state = PlayerState.Dash;
        stats.CurrentDashCount--;
        anim.SetBool("IsDash", true);

        //대쉬 이동
        Vector3 direction = transform.forward.normalized;
        rigid.velocity = direction * stats.CurrentDashDistance * 2;

        //대쉬 시간(하드 코딩 나중에 재구현 해야함)
        yield return new WaitForSeconds(0.5f);

        //대쉬 종료
        state = PlayerState.Idle;
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