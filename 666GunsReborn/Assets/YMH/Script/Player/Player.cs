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

    // 플레이어 스탯
    protected float maxHealth;
    protected float currentHealth;
    protected float moveSpeed;
    protected int currentDashCount;
    protected int maxDashCount;
    protected float dashDistance;
    protected float dashCooldown;
    protected float currentDashCooldown;

    // 플레이어 컴포넌트
    protected PlayerData playerData;
    protected Rigidbody rigid;
    protected Animator anim;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public virtual void Init()
    {
        playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");

        maxHealth = playerData.health;
        currentHealth = maxHealth;
        moveSpeed = playerData.moveSpeed;
        maxDashCount = playerData.dashCount;
        currentDashCount = maxDashCount;
        dashDistance = playerData.dashDistance;
        dashCooldown = playerData.dashCooldown;
        currentDashCooldown = 0;
        state = PlayerState.Idle;
    }

    protected virtual void Update()
    {
        //대쉬 쿨타임 계산
        if(currentDashCount <= maxDashCount)
        {
            currentDashCooldown += Time.deltaTime;
            if (currentDashCooldown >= dashCooldown)
            {
                currentDashCooldown = 0;
                currentDashCount++;
            }
        }
    }

    public virtual void Move(Vector3 direction)
    {
        //대쉬 상태에서는 이동 불가
        if (state == PlayerState.Dash)
            return;

        //입력값이 없을 경우
        if (direction == Vector3.zero)
        {
            if (state == PlayerState.Move)
            {
                state = PlayerState.Idle;
            }
        }

        //방향 전환
        LookAt(direction);
        //속도값 적용
        rigid.velocity = direction * moveSpeed;
        //애니메이션
        anim.SetFloat("Speed", direction.magnitude);

        //경사면 이동
        if (OnSlope())
        {
            rigid.AddForce(Vector3.down * 10, ForceMode.Acceleration);
        }
    }
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

    public virtual void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public virtual void Dash()
    {
        //대쉬 상태에서 대쉬 안됨
        if (state == PlayerState.Dash)
            return;

        //대쉬 개수 확인
        if (currentDashCount <= 0)
            return;

        Debug.Log("대쉬");

        StartCoroutine(DashCoroutine());
    }

    protected virtual IEnumerator DashCoroutine()
    {
        //상태 변경
        state = PlayerState.Dash;
        currentDashCount--;
        anim.SetBool("IsDash", true);

        //대쉬 이동
        Vector3 direction = transform.forward.normalized;
        rigid.velocity = direction * dashDistance * 2;

        //대쉬 시간
        yield return new WaitForSeconds(0.5f);

        //대쉬 종료
        state = PlayerState.Idle;
        anim.SetBool("IsDash", false);
    }
}
