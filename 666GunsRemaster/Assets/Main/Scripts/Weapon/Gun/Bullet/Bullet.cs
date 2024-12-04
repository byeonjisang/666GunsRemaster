using System.Collections;
using UnityEngine;

namespace Gun.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private BulletObjectPool bulletObjectPool;

        public GunData gunData { protected get; set; }    //총 데이터

        protected float damage;                 //데미지
        protected float speed;                  //속도
        protected float range;                  //범위
        protected float currentPenetration;     //남은 관통력
        protected float penetration;            //관통력
        protected LayerMask blockObject;        //막히는 물체
        protected int reverse;                  //이미지가 꺼구로 되어 있는가

        protected SpriteRenderer sprite;
        protected Rigidbody2D rigid;

        protected virtual void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
            bulletObjectPool = GetComponentInParent<BulletObjectPool>();
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
            currentPenetration = penetration;
        }
        public virtual void Shoot()
        {
            StartCoroutine(ShootBullet());
        }
        protected virtual IEnumerator ShootBullet()
        {
            yield return null;

            reverse = transform.localScale.x == 1 ? -1 : 1;
            rigid.AddForce(transform.right * speed * reverse, ForceMode2D.Impulse);
        }
        public virtual void Shoot(int index)
        {

        }
        protected virtual IEnumerator ShootBullet(int index)
        {
            yield return null;
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            //막히는 물체 확인 후 충돌 처리
            if((blockObject.value & ( 1 << collision.gameObject.layer)) > 0)
            {
                currentPenetration -= 1;

                if(currentPenetration <= 0)
                {
                    rigid.AddForce(Vector2.zero, ForceMode2D.Force);
                    bulletObjectPool.ReturnBullet(this.gameObject);
                }
            }

            //맵 밖으로 나갈 시 총알 반환
            //if (collision.CompareTag("Wall")) 
            //{
            //    bulletObjectPool.ReturnBullet(this.gameObject);
            //}
        }
    }
}