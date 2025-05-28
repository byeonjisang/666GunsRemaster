using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject
{
    private IObjectPool<GameObject> pool;
    private Rigidbody rigid;

    private float damage;
    private float maxDistance = 50f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    public void SetInfo(float damage, float speed)
    {
        this.damage = damage;

        rigid.velocity = Vector3.forward * speed;
        float lifeTime = maxDistance / speed;
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    public void ReturnToPool()
    {
        pool.Release(gameObject); // 충돌 시 풀로 반환
    }

    public void ResetObject()
    {
        rigid.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(GameManager.Instance._gameMode){
            case GameMode.WEAPONTEST:
                OnCollisionBulletInWeaponTest(other);
                break;
            case GameMode.INGAME:
                OnCollisionBulletInInGame(other);
                break;
            default:
                break;
        }
    }

    private void OnCollisionBulletInWeaponTest(Collider other){

    }

    private void OnCollisionBulletInInGame(Collider other){
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyTest>().OnDamge((int)damage);
            ReturnToPool();
        }
    }
}