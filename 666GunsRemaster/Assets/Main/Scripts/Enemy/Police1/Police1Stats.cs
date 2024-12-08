using Character.Player;
using Gun;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Police1Stats : MonoBehaviour
{
    public Police1 police1;
    public LayerMask playerLayer;

    private Transform player;
    private SpriteRenderer sprite;
    private Animator animator;

    public GameObject[] gunPrefabs; // �ѱ� ������ �迭
    [Range(0f, 1f)] public float dropChance = 0.05f; // ��� Ȯ��

    [SerializeField]
    private bool isInAttackRange = false;

    private bool isDead = false;

    [SerializeField] private float attackCooldown = 0.8f; // ���� ��ٿ� �ð� (��)
    private float lastAttackTime = 0f; // ������ ���� �ð��� ���

    private Color hitEffect = new Color(1, 0, 0, 1); //�ǰݽ� ����

    // ��ã�� ����
    private NavMeshAgent agent;

    public void SetData(Police1 data)
    {
        police1 = data;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = police1.GetMoveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = false;

        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DetectPlayer();

        if (player != null && !isDead)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= police1.GetAttackRange)
            {
                // �����Ÿ� �ȿ� ������ �����ϰ�, ������ ����
                isInAttackRange = true;
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);
                StopMovement();

                //DealDamageToPlayer();
            }
            else
            {
                isInAttackRange = false;
                SetAgentPosition();
                animator.SetBool("Attack", false);
                animator.SetBool("Walk", true);
            }

            // ���� ������ �÷��̾ ���� ����
            sprite.flipX = player.position.x > transform.position.x;
        }
    }

    private void DealDamageToPlayer()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, police1.GetAttackRange, playerLayer);
        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // �÷��̾�� ������ ����
                hit.GetComponent<PlayerController>().SetHp(police1.GetDamage());
                Debug.Log("�÷��̾�� �ٰŸ� ������ ���߽��ϴ�.");

                lastAttackTime = Time.time; // ���� �ð��� ������Ʈ
                break; // ù ��° �÷��̾�Ը� �������� ������ �� ����
            }
        }
    }

    public void PlayAttackSound()
    {
        //�Ҹ� ���
        SoundManager.instance.PlayEffectSound(9);
    }

    private void DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, police1.GetSightRange, Vector2.zero, 0, playerLayer);

        if (hit.collider != null)
        {
            player = hit.transform;
            SetAgentPosition();
        }
        else
        {
            player = null;
        }
    }

    void SetAgentPosition()
    {
        if (!isDead && player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    private void StopMovement()
    {
        agent.isStopped = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (police1.GetHp() <= 0f)
            {
                //�״� �Ҹ� ���
                SoundManager.instance.PlayEffectSound(10);
                animator.SetBool("Walk", false);
                animator.SetBool("Die", true);
                isDead = true;
                StartCoroutine(DieAndDestroy());
            }
            else
            {
                police1.SetHp(police1.GetHp() - WeaponManager.instance.GetDamage());
                StartCoroutine(Unbeatable());
            }
            Debug.Log("���� ü�� :: " + police1.GetHp());
        }
    }

    private IEnumerator DieAndDestroy()
    {
        yield return new WaitForSeconds(1f);

        if (gunPrefabs.Length > 0 && Random.value <= dropChance)
        {
            // �������� �ѱ� ����
            int randomIndex = Random.Range(0, gunPrefabs.Length);
            GameObject selectedGun = gunPrefabs[randomIndex];

            // �ѱ� ���
            Instantiate(selectedGun, this.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private IEnumerator Unbeatable()
    {
        Color saveColor = sprite.color;
        sprite.color = hitEffect;
        yield return new WaitForSeconds(0.5f);
        sprite.color = saveColor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, police1.GetAttackRange);
    }
}
