using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police1Stats : MonoBehaviour
{
    public Police1 police1;
    public LayerMask playerLayer;    // �÷��̾ ���� ���̾�

    private Transform player;

    [SerializeField]
    private bool isInAttackRange = false;

    //��ã�� ����
    NavMeshAgent agent;
    public void SetData(Police1 data)
    {
        police1 = data;
    }

    //������ ������ ����� �ϱ� ����.
    public void DebugMonsterInfo()
    {
        Debug.Log("���� �̸� :: " + police1.GetMonsterName);
        Debug.Log("���� ü�� :: " + police1.GetHp);
        Debug.Log("���� ���ݷ� :: " + police1.GetDamage);
        Debug.Log("���� �þ� :: " + police1.GetSightRange);
        Debug.Log("���� �̵��ӵ� :: " + police1.GetMoveSpeed);
        Debug.Log("���� �����Ÿ� :: " + police1.GetAttackRange);
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police1.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= police1.GetAttackRange)
            {
                // �����Ÿ� �ȿ� ������ �����ϰ�, ������ ����
                isInAttackRange = true;
                
            }
            else
            {
                // �����Ÿ� ���̸� �������� �ٽ� ����
                isInAttackRange = false;
            }

            // �����Ÿ� ���̸� ���Ͱ� �÷��̾ ����
            if (!isInAttackRange)
            {
                SetAgentPosition();
            }
            else
            {
                StopMovement();  // �����Ÿ� �ȿ� ������ ������ ���߱�
            }
        }
    }

    // CircleCast2D�� �̿��Ͽ� �÷��̾� ����
    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, police1.GetSightRange, Vector2.zero, 0, playerLayer);

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
        agent.isStopped = false;

        agent.SetDestination(new Vector3(player.position.x, player.position.y,
            transform.position.z));
    }

    // �������� ���ߴ� �Լ�
    private void StopMovement()
    {
        agent.isStopped = true;
    }
}
