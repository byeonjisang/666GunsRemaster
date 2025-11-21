using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    public float attackRange = 2f;
    public float attackCoolTime = 1.5f;
    private float lastAttackTime;

    //�ٰŸ� ���ݿ� ���õ� ����
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damageAmount = 10;

    protected override void Start()
    {
        base.Start();
        animator.Play("Idle");
        state = EnemyState.Chase;
    }

    protected override void HandleIdle() { }

    protected override void HandleChase()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        //�ִϸ��̼� ���
        animator.SetBool("isRun", true);
        animator.SetBool("isAttack", false);
        
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
            animator.SetBool("isAttack", true);
            animator.SetBool("isRun", false);
            lastAttackTime = Time.time;
            //TryDealDamage();
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
                hit.GetComponent<Character.Player.Player>().TakeDamage(damageAmount);
            }
        }
    }


    protected override void HandleReload() { }
}

