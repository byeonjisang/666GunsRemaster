using Gun;
using Gun.Bullet;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private SpriteRenderer sprite;

    public SpriteRenderer gunSprite;

    private Animator animator;

    [SerializeField]
    private bool isInAttackRange = false;

    private bool isDead = false;

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
        //Debug.Log("���� �̸� :: " + police2.GetMonsterName);
        //Debug.Log("���� ü�� :: " + police2.GetHp);
        //Debug.Log("���� ���ݷ� :: " + police2.GetDamage);
        //Debug.Log("���� �þ� :: " + police2.GetSightRange);
        //Debug.Log("���� �̵��ӵ� :: " + police2.GetMoveSpeed);
        //Debug.Log("���� �����Ÿ� :: " + police2.GetAttackRange);
    }

    //// ���� �޼���
    //public Police2Stats Clone(GameObject newObject)
    //{
    //    Police2Stats clone = newObject.AddComponent<Police2Stats>();
    //    clone.animator = this.animator;

    //    // NavMeshAgent �缳��
    //    NavMeshAgent agent = newObject.GetComponent<NavMeshAgent>();

    //    if (agent == null)
    //    {
    //        agent = newObject.AddComponent<NavMeshAgent>();  // NavMeshAgent�� ������ �߰�
    //    }

    //    // ���� agent ���� ����
    //    agent.speed = police2.GetMoveSpeed;
    //    agent.updateRotation = false;
    //    agent.updateUpAxis = false;
    //    agent.isStopped = false;

    //    return clone;
    //}
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police2.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        sprite = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            Vector3 targetDistancePos = player.transform.position - transform.position;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= police2.GetAttackRange)
            {
                // �����Ÿ� �ȿ� ������ �����ϰ�, ������ ����
                isInAttackRange = true;
                TryShoot();
                StopMovement();
            }
            else
            {
                // �����Ÿ� ���̸� �������� �ٽ� ����
                isInAttackRange = false;
                SetAgentPosition();
            }

            if (targetDistancePos.x < 0)
            {
                sprite.flipX = false;
                gunSprite.flipY = true;
            }
            else
            {
                sprite.flipX = true;
                gunSprite.flipY = false;
            }
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
        agent.speed = police2.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        // NavMeshAgent ����
        agent.enabled = false;  // ���� ��Ȱ��ȭ
        agent.enabled = true;   // �ٽ� Ȱ��ȭ�Ͽ� NavMesh�� �翬��

        // ��� ���ʱ�ȭ
        agent.ResetPath();      // ��� �ʱ�ȭ
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
            Debug.Log("Player null");
        }
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
        Debug.Log("Chasing Stopped");
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (police2.GetHp() <= 0f)
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
                police2.SetHp(police2.GetHp() - WeaponManager.instance.GetDamage());
            }
            Debug.Log("���� ü�� :: " + police2.GetHp());
        }
    }

    private IEnumerator DieAndDestroy()
    {
        //���� ����
        agent.isStopped = true;

        // ��� �ִϸ��̼��� ����Ǵ� �ð���ŭ ��� (��: 2��)
        yield return new WaitForSeconds(2f);  // �ִϸ��̼� ���̿� �°� ����

        // ������Ʈ ����
        Destroy(gameObject);
    }
}
