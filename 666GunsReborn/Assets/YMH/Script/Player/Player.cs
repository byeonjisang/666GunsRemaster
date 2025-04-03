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
        //대쉬 상태에서는 이동 불가
        if (state == PlayerState.Dash)
            return;

        //입력값이 없을 경우
        if (direction == Vector3.zero)
        {
            if (OnSlope())
            {
                // 경사면에서 정지 시 미끄럼 방지
                rigid.velocity = Vector3.zero;
            }
            else
            {
                // 평지에서 정지
            }
            state = PlayerState.Idle;
        }
        else
        {
            //방향 전환
            LookAt(direction);
            //속도값 적용
            // 경사면 이동 보정 추가
            if (OnSlope())
            {
                Vector3 slopeDirection = Vector3.ProjectOnPlane(direction, GetSlopeNormal());
                rigid.velocity = slopeDirection * stats.CurrentMoveSpeed;
            }
            else
            {
                Vector3 moveVelocity = new Vector3(direction.x, 0f, direction.z) * stats.CurrentMoveSpeed;
                rigid.velocity = new Vector3(moveVelocity.x, rigid.velocity.y, moveVelocity.z);
            }
        }

        //애니메이션
        anim.SetFloat("Speed", direction.magnitude);
    }
    //플레이어 이동시 경사면 보정
    private Vector3 GetSlopeNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            return hit.normal;
        }
        return Vector3.up;
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
    private bool OnSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            return angle > 0 && angle < 45;  // 45도 이하의 경사만 이동 가능
        }
        return false;
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