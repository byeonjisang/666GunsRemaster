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
        Debug.Log(rigid.velocity.y);
        if (state == PlayerState.Dash)
            return;

        var slopeInfo = GetSlopeInfo(direction);

        if (direction.sqrMagnitude < 0.001f)
        {
            if (slopeInfo.onSlope && IsGrounded())
                rigid.velocity = Vector3.zero;

            state = PlayerState.Idle;

            Debug.Log("����");
        }
        else
        {
            Debug.Log("�̵�");
            if (slopeInfo.angle > 45f && IsGrounded())
            {
                Debug.Log("���� �ʹ� ���ĸ�");
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

    //�� ���� �ִ��� üũ
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    //�÷��̾� �̵��� ���� ����
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

    //�÷��̾� ȸ��
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