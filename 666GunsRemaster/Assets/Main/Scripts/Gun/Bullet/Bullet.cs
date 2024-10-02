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
            //보고 있는 방향에 힘을 주기 위한 코드
            reverse = sprite.flipX == true ? -1 : 1;

            //현재 남은 관통력 초기화
            currentPenetration = penetration;
            Debug.Log(currentPenetration);
        }
        protected virtual void FixedUpdate()
        {
            //쏜 방향으로 이동
            this.transform.Translate(transform.right * reverse * this.speed * Time.deltaTime);
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            //막히는 물체 확인 후 충돌 처리
            if((blockObject.value & ( 1 << collision.gameObject.layer)) > 0)
            {
                currentPenetration -= 1;

                if(currentPenetration <= 0)
                    Pool.Release(this);
            }
        }
    }
}