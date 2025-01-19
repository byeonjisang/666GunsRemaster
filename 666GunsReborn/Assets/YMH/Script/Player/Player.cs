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
    // �÷��̾� ����
    protected float maxHealth;
    protected float currentHealth;
    protected float moveSpeed;
    protected int dashCount;
    protected int currentDashCount;
    protected float dashDistance;
    protected float dashCooldown;

    // �÷��̾� ����
    protected PlayerState state;

    // �÷��̾� ������Ʈ
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
        //�ʱ�ȭ
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

        //���� �ʱ�ȭ
        state = PlayerState.Idle;
    }

    public virtual void Move(Vector3 direction)
    {
        //�Է°��� ���� ���
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

        //���� ��ȯ
        LookAt(direction);
        //�ӵ��� ����
        rigid.velocity = direction * moveSpeed;
        //���º���
        state = PlayerState.Move;
        //�ִϸ��̼�
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
