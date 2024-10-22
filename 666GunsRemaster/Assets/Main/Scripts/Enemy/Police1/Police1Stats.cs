using System.Collections;
using System.Collections.Generic;
using Gun;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Police1Stats : MonoBehaviour
{
    public Police1 police1;
    public LayerMask playerLayer;    // �÷��̾ ���� ���̾�

    private Transform player;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField]
    private bool isInAttackRange = false;

    private bool isDead = false;

    //��ã�� ����
    NavMeshAgent agent;
    public void SetData(Police1 data)
    {
        police1 = data;
    }

    //������ ������ ����� �ϱ� ����.
    public void DebugMonsterInfo()
    {
        //Debug.Log("���� �̸� :: " + police1.GetMonsterName);
        //Debug.Log("���� ü�� :: " + police1.GetHp);
        //Debug.Log("���� ���ݷ� :: " + police1.GetDamage);
        //Debug.Log("���� �þ� :: " + police1.GetSightRange);
        //Debug.Log("���� �̵��ӵ� :: " + police1.GetMoveSpeed);
        //Debug.Log("���� �����Ÿ� :: " + police1.GetAttackRange);
    }

    // ���� �޼���
    //public Police1Stats Clone(GameObject newObject)
    //{
    //    Police1Stats clone = newObject.AddComponent<Police1Stats>();
    //    clone.animator = this.animator;

    //    // NavMeshAgent �缳��
    //    NavMeshAgent agent = newObject.GetComponent<NavMeshAgent>();

    //    if (agent == null)
    //    {
    //        agent = newObject.AddComponent<NavMeshAgent>();  // NavMeshAgent�� ������ �߰�
    //    }

    //    // ���� agent ���� ����
    //    agent.speed = police1.GetMoveSpeed;
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;
    //    agent.isStopped = false;

    //    // NavMeshAgent ����
    //    agent.enabled = false;  // ���� ��Ȱ��ȭ
    //    agent.enabled = true;   // �ٽ� Ȱ��ȭ�Ͽ� NavMesh�� �翬��

    //    // ��� ���ʱ�ȭ
    //    agent.ResetPath();      // ��� �ʱ�ȭ

    //    return clone;
    //}

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police1.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        float currentHp = police1.GetCurrentHp();
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            Vector3 targetDistancePos = player.transform.position - transform.position;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= police1.GetAttackRange)
            {
                // �����Ÿ� �ȿ� ������ �����ϰ�, ������ ����
                isInAttackRange = true;
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);
                StopMovement();
            }
            else
            {
                // �����Ÿ� ���̸� �������� �ٽ� ����
                isInAttackRange = false;
                SetAgentPosition();
                animator.SetBool("Attack", false);
                animator.SetBool("Walk", true);
            }

            if (targetDistancePos.x < 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
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
            Debug.Log("Player null");
        }
    }

    void OnEnable()
    {
        // NavMeshAgent �缳��
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            agent = this.AddComponent<NavMeshAgent>();  // NavMeshAgent�� ������ �߰�
        }

        // ���� agent ���� ����
        agent.speed = police1.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        // NavMeshAgent ����
        agent.enabled = false;  // ���� ��Ȱ��ȭ
        agent.enabled = true;   // �ٽ� Ȱ��ȭ�Ͽ� NavMesh�� �翬��

        // ��� ���ʱ�ȭ
        agent.ResetPath();      // ��� �ʱ�ȭ
    }

    void SetAgentPosition()
    {
        if (!isDead)
        {
            agent.isStopped = false;

            agent.SetDestination(new Vector3(player.position.x, player.position.y,
                transform.position.z));
        }
    }

    // �������� ���ߴ� �Լ�
    private void StopMovement()
    {
        agent.isStopped = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (police1.GetHp() <= 0f)
            {
                //�ִϸ��̼�
                animator.SetBool("Walk", false);
                animator.SetBool("Die", true);

                isDead = true;

                // ��� �ִϸ��̼��� ���� �� ������Ʈ�� ����
                StartCoroutine(DieAndDestroy());
            }
            else
            {
                police1.SetHp(police1.GetHp() - WeaponManager.instance.GetDamage());
            }
            Debug.Log("���� ü�� :: " + police1.GetHp());
            Debug.Log(WeaponManager.instance.GetDamage());
        }
    }

    private IEnumerator DieAndDestroy()
    {
        // ��� �ִϸ��̼��� ����Ǵ� �ð���ŭ ��� (��: 2��)
        yield return new WaitForSeconds(1f);  // �ִϸ��̼� ���̿� �°� ����

        // ������Ʈ ����
        Destroy(gameObject);
    }
}
