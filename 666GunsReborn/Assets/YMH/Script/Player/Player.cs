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
    protected float maxHealth;
    protected float currentHealth;
    protected float moveSpeed;
    protected int currentDashCount;
    protected int maxDashCount;
    protected float dashDistance;
    protected float dashCooldown;
    protected float currentDashCooldown;

    // �÷��̾� ������Ʈ
    protected PlayerData playerData;
    protected Rigidbody rigid;
    protected Animator anim;

    // �÷��̾� ���� �� ȹ�� ����
    protected int coin = 0;

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
        //�뽬 ��Ÿ�� ���
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

#region Player Move
    public virtual void Move(Vector3 direction)
    {
        //�뽬 ���¿����� �̵� �Ұ�
        if (state == PlayerState.Dash)
            return;

        //�Է°��� ���� ���
        if (direction == Vector3.zero)
        {
            if (state == PlayerState.Move)
            {
                state = PlayerState.Idle;
            }
        }

        //���� ��ȯ
        LookAt(direction);
        //�ӵ��� ����
        // ���� �̵� ���� �߰�
        if (OnSlope())
        {
            Vector3 slopeDirection = Vector3.ProjectOnPlane(direction, GetSlopeNormal());
            rigid.velocity = slopeDirection * moveSpeed;
        }
        else
        {
            Vector3 currentVelocity = rigid.velocity;  
            Vector3 moveVelocity = direction * moveSpeed;

            //rigid.velocity = direction * moveSpeed;
            rigid.velocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);
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

    public virtual void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public virtual void Dash()
    {
        //�뽬 ���¿��� �뽬 �ȵ�
        if (state == PlayerState.Dash)
            return;

        //�뽬 ���� Ȯ��
        if (currentDashCount <= 0)
            return;

        Debug.Log("�뽬");

        StartCoroutine(DashCoroutine());
    }

    protected virtual IEnumerator DashCoroutine()
    {
        //���� ����
        state = PlayerState.Dash;
        currentDashCount--;
        anim.SetBool("IsDash", true);

        //�뽬 �̵�
        Vector3 direction = transform.forward.normalized;
        rigid.velocity = direction * dashDistance * 2;

        //�뽬 �ð�(�ϵ� �ڵ� ���߿� �籸�� �ؾ���)
        yield return new WaitForSeconds(0.5f);

        //�뽬 ����
        state = PlayerState.Idle;
        anim.SetBool("IsDash", false);
    }

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