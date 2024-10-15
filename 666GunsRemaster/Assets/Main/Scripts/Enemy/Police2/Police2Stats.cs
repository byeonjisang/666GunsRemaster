using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Police2Stats : MonoBehaviour
{
    public Police2 police2;
    public Transform firePoint;  // �Ѿ��� �߻�� ��ġ
    public GameObject bulletPrefab;  // �߻��� �Ѿ� ������
    public LayerMask playerLayer;    // �÷��̾ ���� ���̾�

    public float attackCooldown = 1f;    // ���� ����
    private float lastAttackTime;

    private Transform player;

    //��ã�� ����
    NavMeshAgent agent;

    // �ܺο��� Police2 �����͸� �޾Ƽ� �����ϴ� �޼���
    public void SetData(Police2 data)
    {
        police2 = data;
    }

    // ������ ������ ����� �ϱ� ����.
    public void DebugMonsterInfo()
    {
        Debug.Log("���� �̸� :: " + police2.GetMonsterName);
        Debug.Log("���� ü�� :: " + police2.GetHp);
        Debug.Log("���� ���ݷ� :: " + police2.GetDamage);
        Debug.Log("���� �þ� :: " + police2.GetSightRange);
        Debug.Log("���� �̵��ӵ� :: " + police2.GetMoveSpeed);
        Debug.Log("���� �����Ÿ� :: " + police2.GetAttackRange);
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police2.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        DetectPlayer();
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // �����Ÿ� ���� ������ ��� ����
            if (distanceToPlayer <= police2.GetAttackRange)
            {
                TryShoot();
            }
        }
    }

    // CircleCast2D�� �̿��Ͽ� �÷��̾� ����
    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, police2.GetSightRange, Vector2.zero, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform;  // �÷��̾ �����ϸ� ����

            //�÷��̾� ���� ����
            SetAgentPosition();
        }
        else
        {
            player = null;  // �÷��̾ �������� ���ϸ� null
        }
    }
    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(player.position.x, player.position.y,
            transform.position.z));
    }

    // �����Ÿ��� ������ �� �÷��̾ ���� ���� �߻�
    private void TryShoot()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Shoot();
            lastAttackTime = Time.time;
        }
    }

    // �Ѿ� �߻� �޼���
    private void Shoot()
    {
        if (firePoint != null && bulletPrefab != null)
        {
            // �Ѿ��� �����ϰ� �÷��̾ ���� �߻�
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            if (bulletComponent != null)
            {
                //bulletComponent.SetTarget(player.position);
            }

            Debug.Log("���� �߻��߽��ϴ�!");
        }
    }

    // ���� ������ �׷� �����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, police2.GetSightRange);
    }
}
