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
    // �÷��̾� ����
    protected PlayerState state;
    // �÷��̾� ����
    protected PlayerStats stats;

    // �÷��̾� ������Ʈ
    protected Rigidbody rigid;
    protected Animator anim;

    // �÷��̾� ���� �� ȹ�� ����
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
        
        //���� �ʱ�ȭ
        stats.Init(playerData);
        state = PlayerState.Idle;
    }

    protected virtual void Update()
    {
        //�뽬 ��Ÿ�� ���
        stats.DashCountCoolDown();
    }

    #region Player Move
    public virtual void Move(Vector3 direction)
    {
        //�뽬 ���¿����� �̵� �Ұ�
        if (state == PlayerState.Dash)
            return;

        //�Է°��� ���� ���
        if (direction == Vector3.zero)
        {
            if (OnSlope())
            {
                // ���鿡�� ���� �� �̲��� ����
                rigid.velocity = Vector3.zero;
            }
            else
            {
                // �������� ����
            }
            state = PlayerState.Idle;
        }
        else
        {
            //���� ��ȯ
            LookAt(direction);
            //�ӵ��� ����
            // ���� �̵� ���� �߰�
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

        //�ִϸ��̼�
        anim.SetFloat("Speed", direction.magnitude);
    }
    //�÷��̾� �̵��� ���� ����
    private Vector3 GetSlopeNormal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            return hit.normal;
        }
        return Vector3.up;
    }
    //�÷��̾� ȸ��
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
            return angle > 0 && angle < 45;  // 45�� ������ ��縸 �̵� ����
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
        //�뽬 ���¿��� �뽬 �ȵ�
        if (state == PlayerState.Dash)
            return;

        //�뽬 ���� Ȯ��
        if (stats.CurrentDashCount <= 0)
            return;

        Debug.Log("�뽬");

        StartCoroutine(DashCoroutine());
    }

    protected virtual IEnumerator DashCoroutine()
    {
        //���� ����
        state = PlayerState.Dash;
        stats.CurrentDashCount--;
        anim.SetBool("IsDash", true);

        //�뽬 �̵�
        Vector3 direction = transform.forward.normalized;
        rigid.velocity = direction * stats.CurrentDashDistance * 2;

        //�뽬 �ð�(�ϵ� �ڵ� ���߿� �籸�� �ؾ���)
        yield return new WaitForSeconds(0.5f);

        //�뽬 ����
        state = PlayerState.Idle;
        anim.SetBool("IsDash", false);
    }
    #endregion

    // ���� �浹 ó��
    protected virtual void OnTriggerEnter(Collider other)
    {
        //���� ȹ��
        if (other.CompareTag("Coin"))
        {
            Coin coin = other.GetComponent<Coin>();
            this.coin += coin.GetAmount();
            Destroy(other.gameObject);
        }
    }
}