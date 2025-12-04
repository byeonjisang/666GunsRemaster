using UnityEngine;
using UnityEngine.Pool;

namespace Weapon.Bullet
{
    public class BulletBase : MonoBehaviour, IPooledObject
    {
        protected IObjectPool<GameObject> pool;
        protected Rigidbody rigid;

        protected float power;
        protected float maxDistance = 50f;

        protected virtual void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public void SetPool(IObjectPool<GameObject> pool)
        {
            this.pool = pool;
        }

        public virtual void Initialization(float power, float speed)
        {
            this.power = power;

            rigid.velocity = transform.forward * speed;
            float lifeTime = maxDistance / speed;
            Invoke(nameof(ReturnToPool), lifeTime);
        }

        public virtual void ReturnToPool()
        {
            pool.Release(gameObject); // 충돌 시 풀로 반환
        }

        public virtual void ResetObject()
        {
            rigid.velocity = Vector3.zero;
        }

        protected virtual void OnDisable()
        {
            rigid.velocity = Vector3.zero;
        }
    }
}