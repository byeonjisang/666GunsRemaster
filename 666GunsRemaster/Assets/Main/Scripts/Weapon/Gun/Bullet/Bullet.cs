using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Gun.Bullet
{
    public class Bullet : MonoBehaviour
    {
        public IObjectPool<Bullet> Pool { get; set; }   //오브젝트 풀
        public GunData gunData { private get; set; }    //총 데이터

        protected float damage;                 //데미지
        protected float speed;                  //속도
        protected float range;                  //범위
        [SerializeField]
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
        }

        protected virtual void Start()
        {
            damage = gunData.damage;
            speed = gunData.bulletSpeed;
            range = gunData.range;
            penetration = gunData.penetration;
            blockObject = gunData.blockObject;

            reverse = transform.localScale.x == 1 ? -1 : 1;
            rigid.AddForce(transform.right * speed * reverse, ForceMode2D.Impulse);
        }
        protected virtual void OnEnable()
        {
            //보고 있는 방향에 힘을 주기 위한 코드
            reverse = transform.localScale.x == 1 ? -1 : 1;

            //현재 남은 관통력 초기화
            currentPenetration = penetration;

            rigid.AddForce(transform.right * speed * reverse, ForceMode2D.Impulse);
            rigid.AddForce(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), ForceMode2D.Impulse);
        }
        /// <summary>
        /// 데미지 반환
        /// </summary>
        public float GetDamage()
        {
            return damage;
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
                    Pool.Release(this);
                }
                    
            }
        }
    }
}