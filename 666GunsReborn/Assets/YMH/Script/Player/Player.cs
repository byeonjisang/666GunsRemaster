using System.Collections;
using System.Collections.Generic;
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
    // 플레이어 스탯
    protected float maxHealth;
    protected float currentHealth;
    protected float moveSpeed;
    protected int dashCount;
    protected int currentDashCount;
    protected float dashDistance;
    protected float dashCooldown;

    // 플레이어 상태
    protected PlayerState state;

    // 플레이어 컴포넌트
    protected PlayerData playerData;
    protected Rigidbody rigid;
    protected Animator anim;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        //초기화
        Init();
    }

    protected virtual void Init()
    {
        playerData = Resources.Load<PlayerData>($"Datas/Player/{this.GetType().ToString()}");

        maxHealth = playerData.health;
        currentHealth = maxHealth;
        moveSpeed = playerData.moveSpeed;
        dashCount = playerData.dashCount;
        currentDashCount = dashCount;
        dashDistance = playerData.dashDistance;
        dashCooldown = playerData.dashCooldown;

        //상태 초기화
        state = PlayerState.Idle;
    }

    public virtual void Move(Vector3 direction)
    {
        //입력값이 없을 경우
        if(direction == Vector3.zero)
        {
            if(state == PlayerState.Move)
            {
                rigid.velocity = Vector3.zero;
                anim.SetFloat("Speed", 0);
                state = PlayerState.Idle;
            }
            return;
        }

        //방향 전환
        LookAt(direction);
        //속도값 적용
        rigid.velocity = direction * moveSpeed;
        //상태변경
        state = PlayerState.Move;
        //애니메이션
        anim.SetFloat("Speed", rigid.velocity.magnitude);
    }
    private void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rigid.rotation = targetAngle;
        }
    }

    public virtual void Dash()
    {

    }
}
