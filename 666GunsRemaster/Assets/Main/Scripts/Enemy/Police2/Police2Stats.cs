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
    public GameObject bulletPrefab;  // �߻��� �Ѿ� ������
    public LayerMask playerLayer;    // �÷��̾ ���� ���̾�

    public float attackCooldown = 2f;    // ���� ����
    private float lastAttackTime;

    private Transform player;
    private SpriteRenderer sprite;

    public SpriteRenderer gunSprite;

    private Animator animator;

    public GameObject[] gunPrefabs; // �ѱ� ������ �迭
    [Range(0f, 1f)] public float dropChance = 0.1f; // ��� Ȯ��

    [SerializeField]
    private bool isInAttackRange = false;

    private bool isDead = false;
    private bool isTarget = false;

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

        gunSprite.gameObject.SetActive(true);
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
                animator.SetBool("IsAttack", false);
                SetAgentPosition();
            }

            if (targetDistancePos.x < 0)
            {
                sprite.flipX = false;
                gunSprite.flipY = false;
            }
            else
            {
                sprite.flipX = true;
                gunSprite.flipY = false;
            }
        }
    }

    void FixedUpdate()
    {
        RotateGunTowardsTarget();
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

    private void RotateGunTowardsTarget()
    {
        // ��ó ���� �ִ��� Ȯ��
        if (player != null)
        {
            isTarget = true;

            // ������ �Ÿ� �� ���� ���
            Vector3 targetDirection = player.position - transform.position;

            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg + 270;
            if (targetDirection.x < 0)
            {
                gunSprite.flipX = true;
                //weaponManager.transform.localScale = new Vector3(1, 1, 1);

                if (angle > 360)
                    angle -= 360;
            }
            else
            {
                gunSprite.flipX = false;
                //weaponManager.transform.localScale = new Vector3(-1, 1, 1);

            }

            gunSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            isTarget = false;
            gunSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    }

    // �����Ÿ��� ������ �� �÷��̾ ���� ���� �߻�
    private void TryShoot()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Shoot();
            animator.SetBool("IsAttack", true);
            lastAttackTime = Time.time;
        }
    }

    // �Ѿ� �߻� �޼���
    private void Shoot()
    {
        if (player != null && bulletPrefab != null)
        {
            //�ѼҸ� ���
            SoundManager.instance.PlayEffectSound(8);

            // �Ѿ��� �����ϰ� �÷��̾ ���� �߻�
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bullet.transform.rotation = Quaternion.Euler(0, 0, gunSprite.transform.rotation.eulerAngles.z - 90);
            PoliceBullet bulletComponent = bullet.GetComponent<PoliceBullet>();

            if (bulletComponent != null)
            {
                Debug.Log("�Ѿ� ����");
                bulletComponent.Shoot();
                //bulletComponent.SetTarget(player.position);
            }

            Debug.Log("���� �߻��߽��ϴ�!");
        }
    }

    public void GunDelete()
    {
        //�ѱ� �����
        gunSprite.gameObject.SetActive(false);
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
                //��� �Ҹ� ���
                SoundManager.instance.PlayEffectSound(10);

                //�ִϸ��̼�
                animator.SetBool("Walk", false);
                animator.SetBool("IsAttack", false);
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

        // ��� �ִϸ��̼��� ����Ǵ� �ð���ŭ ���
        yield return new WaitForSeconds(1f);  // �ִϸ��̼� ���̿� �°� ����

        if (gunPrefabs.Length > 0 && Random.value <= dropChance)
        {
            // �������� �ѱ� ����
            int randomIndex = Random.Range(0, gunPrefabs.Length);
            GameObject selectedGun = gunPrefabs[randomIndex];

            // �ѱ� ���
            Instantiate(selectedGun, this.transform.position, Quaternion.identity);
        }

        // ������Ʈ ����
        Destroy(gameObject);
    }
}
