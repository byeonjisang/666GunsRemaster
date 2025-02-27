using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject
{
    private IObjectPool<GameObject> pool;
    private Rigidbody rigid;

    private float damage;
    private float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    public void SetSpeed(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }

    public void ReturnToPool()
    {
        pool.Release(gameObject); // 충돌 시 풀로 반환
    }

    public void ResetObject()
    {
        rigid.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }
}