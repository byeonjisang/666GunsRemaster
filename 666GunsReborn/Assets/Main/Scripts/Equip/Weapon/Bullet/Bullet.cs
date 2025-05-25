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

    /// <summary>
    /// 2025.5.25
    /// 총알은 인스턴싱되는 객체이기에, Update 돌면 안됨.
    /// Player내부에서 전달받아 처리 필요.
    /// </summary>
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
        switch(GameManager.Instance._gameMode){
            case GameMode.WEAPONTEST:
                OnCollisionBulletInWeaponTest(other);
                break;
            case GameMode.INGAME:
                OnCollisionBulletInIngame(other);
                break;
            default:
                break;
        }
    }

    private void OnCollisionBulletInWeaponTest(Collider other){

    }

    private void OnCollisionBulletInIngame(Collider other){
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyTest>().OnDamge((int)damage);
            ReturnToPool();
        }
    }
}