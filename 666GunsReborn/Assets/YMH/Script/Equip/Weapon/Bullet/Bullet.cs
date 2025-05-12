using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject
{
    private IObjectPool<GameObject> pool;
    private Rigidbody rigid;

    private float damage;
    private float speed;
    private float maxDistance = 50f;
    private Vector3 spawnPosition;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    public void SetInfo(float damage, float speed, Vector3 spawnPosition)
    {
        this.damage = damage;
        this.speed = speed;
        this.spawnPosition = spawnPosition;
    }

    public void ReturnToPool()
    {
        pool.Release(gameObject); // 충돌 시 풀로 반환
    }

    public void ResetObject()
    {
        rigid.velocity = Vector3.zero;
    }

    public void Update()
    {
        float traveledDistance = Vector3.Distance(spawnPosition, transform.position);
        if(traveledDistance >= maxDistance)
        {
            ReturnToPool(); // 최대 비행 거리 도달 시 풀로 반환
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyTest>().OnDamge((int)damage);
            ReturnToPool();
        }
    }
}