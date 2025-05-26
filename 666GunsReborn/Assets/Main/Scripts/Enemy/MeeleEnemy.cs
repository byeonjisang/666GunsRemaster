using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackRange = 2f;
    public float attackCoolTime = 1.5f;
    private float lastAttackTime;

    //근거리 공격에 관련된 변수
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damageAmount = 10;

    protected override void Start()
    {
        base.Start();
        animator.Play("Ready");
        state = EnemyState.Chase;
    }

    protected override void HandleIdle() { }

    protected override void HandleChase()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            state = EnemyState.Attack;
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    protected override void HandleAttack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (Time.time - lastAttackTime >= attackCoolTime)
        {
            animator.Play("Attack");
            lastAttackTime = Time.time;
            // 플레이어에 데미지 주기
            TryDealDamage();
        }

        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            state = EnemyState.Chase;
        }
    }
    public void TryDealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                //hit.GetComponent<PlayerHealth>()?.TakeDamage(damageAmount);
            }
        }
    }


    protected override void HandleReload() { }
}

