using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    public float attackRange = 10f;
    public float attackCoolTime = 2f;
    public int bulletMax = 5;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private int currentBullet;
    private float lastAttackTime;
    protected override void Start()
    {
        base.Start();
        currentBullet = bulletMax;
        animator.Play("Ready");
        state = EnemyState.Chase;

        if (firePoint != null)
        {
            firePoint.localPosition = new Vector3(0, 1.0f, 0.5f);
            firePoint.localRotation = Quaternion.identity;
        }
    }

    protected override void HandleIdle() { }

    protected override void HandleChase()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        animator.SetBool("isRun", true);
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
        agent.SetDestination(transform.position); // 정지
        transform.LookAt(player);

        if (Time.time - lastAttackTime >= attackCoolTime)
        {
            if (currentBullet <= 0)
            {
                state = EnemyState.Reload;
                Debug.Log("재장전 중...");
            }
            else
            {
                animator.SetBool("isRun", false);
                animator.Play("Shoot");
                FireBullet();
                //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                currentBullet--;
                lastAttackTime = Time.time;
            }
        }

        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            state = EnemyState.Chase;
        }
    }
    public void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * 10f;
        }

        Destroy(bullet, 3f);
    }

    protected override void HandleReload()
    {
        animator.Play("Reload");
        currentBullet = bulletMax;
        lastAttackTime = Time.time;
        state = EnemyState.Chase;
    }
}
