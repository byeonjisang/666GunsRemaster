using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPooledObject
{
    private IObjectPool<GameObject> pool;
    private Rigidbody rigid;

    private float power;
    private float maxDistance = 50f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }

    public void Initialization(float power, float speed)
    {
        this.power = power;

        rigid.velocity = transform.forward * speed;
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
            Debug.Log("Bullet hit enemy: " + other.name);

            Character.Enemy.Enemy enemyScript = other.GetComponent<Character.Enemy.Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage((int)power);
            }
            ReturnToPool();
        }
    }
}