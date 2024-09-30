using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Gun.Bullet
{

    public class Bullet : MonoBehaviour
    {
        public IObjectPool<Bullet> Pool { get; set; }
        public GunData gunData { private get; set; }

        protected float damage;
        protected float speed;
        protected float range;
        protected float penetration;
        protected LayerMask blockObject;
        protected int reverse;

        protected SpriteRenderer sprite;

        protected virtual void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            damage = gunData.damage;
            speed = gunData.bulletSpeed;
            range = gunData.range;
            penetration = gunData.penetration;
            blockObject = gunData.blockObject;
        }
        protected virtual void OnEnable()
        {
            reverse = sprite.flipX == true ? -1 : 1;
        }
        protected virtual void FixedUpdate()
        {
            this.transform.Translate(transform.right * reverse * this.speed * Time.deltaTime);
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if((blockObject.value & ( 1 << collision.gameObject.layer)) > 0)
            {
                Debug.Log("Block");
                Pool.Release(this);
            }
        }
    }
}