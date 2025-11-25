using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour
    {
        // Rigidbody
        public Rigidbody Rigid { get; private set;}
        // Animator
        public Animator Anim { get; private set; }

        // character 초기화
        protected virtual void Awake()
        {
            Rigid = GetComponent<Rigidbody>();
            Anim = GetComponent<Animator>();
        }

        // 데미지 부여 메서드
        public abstract void TakeDamage(float Damage);

        // 사망 처리 메서드
        protected abstract void Dead();
    }
}
