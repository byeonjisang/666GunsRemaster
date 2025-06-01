using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ������ �ൿ��
/// </summary>
public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Reload,
    Hit,
    Die
}


public abstract class BaseEnemy : MonoBehaviour
{
    public int hp = 100;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Transform player;
    protected EnemyState state = EnemyState.Idle;
    protected bool isDead = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player")?.transform;

        EnemyManager.Instance.RegisterEnemy(this);
    }

    protected virtual void Update()
    {
        if (isDead || player == null) return;

        switch (state)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
            case EnemyState.Reload:
                HandleReload();
                break;
            case EnemyState.Hit:
                HandleHit();
                break;
            case EnemyState.Die:
                HandleDie();
                break;
        }
    }

    /// <summary>
    /// ��� �� �ʿ��� �Լ� ���.
    /// </summary>
    protected abstract void HandleIdle();
    protected abstract void HandleChase();
    protected abstract void HandleAttack();
    protected abstract void HandleReload();
    protected virtual void HandleHit() => animator.Play("Hit");
    protected virtual void HandleDie()
    {
        isDead = true;
        agent.isStopped = true;
        //animator.Play("Die");
        StageManager.Instance.DeadEnemy(gameObject); // ��� ���
        gameObject.SetActive(false); // ��� ���
    }

    /// <summary>
    /// ���
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;
        if (hp <= 0)
        {
            state = EnemyState.Die;
        }
        else
        {
            state = EnemyState.Hit;
        }
    }
}
