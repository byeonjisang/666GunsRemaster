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

    [SerializeField]
    private bool isInAttackRange = false;

    private bool isDead = false;

    // 길찾기 적용
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
                // 사정거리 안에 들어오면 공격하고, 움직임 멈춤
                isInAttackRange = true;
                animator.SetBool("Attack", true);
                animator.SetBool("Walk", false);
                StopMovement();
                Attack();
            }
            else
            {
                isInAttackRange = false;
                SetAgentPosition();
                animator.SetBool("Attack", false);
                animator.SetBool("Walk", true);
            }

            // 적의 방향을 플레이어에 맞춰 조정
            sprite.flipX = player.position.x > transform.position.x;
        }
    }

    private void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, police1.GetAttackRange, playerLayer);
        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))
            {
                // 플레이어에게 데미지 전달
                //hit.GetComponent<PlayerHealth>().TakeDamage(police1.GetDamage());
                Debug.Log("플레이어에게 근거리 공격을 가했습니다.");
            }
        }
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
                animator.SetBool("Walk", false);
                animator.SetBool("Die", true);
                isDead = true;
                StartCoroutine(DieAndDestroy());
            }
            else
            {
                police1.SetHp(police1.GetHp() - WeaponManager.instance.GetDamage());
            }
            Debug.Log("몬스터 체력 :: " + police1.GetHp());
        }
    }

    private IEnumerator DieAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, police1.GetAttackRange);
    }
}
