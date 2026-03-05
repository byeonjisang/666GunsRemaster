using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapon
{
    public class Bottle : MonoBehaviour
    {
        [Header("Settings")]
        // 폭탄 터지는 효과
        [SerializeField] private ParticleSystem _bombEffect;
        // 폭탄 터지는 범위 표시 프리팹
        [SerializeField] private GameObject _bombRadiusPrefab;
        // 병이 날아가는 높이
        [SerializeField] private float _height = 1.5f;
        // 폭탄 터지는 범위
        [SerializeField] private float _bombRadius = 3f;

        private GameObject _bombRadiusEffect;

        /// <summary>
        /// 병폭탄 초기화
        /// </summary>
        public void Init()
        {
            // 병폭탄 터지는 효과 끄기
            _bombEffect.Stop();
        }

        /// <summary>
        /// 병 폭탄을 던지는 메소드
        /// </summary>
        public void Throw(Transform playerTransform, float speed)
        {
            // Enemy로부터 벗어났으니 부모에서 제외
            transform.SetParent(null);
            // 폭탄 터지는 범위 효과 켜기
            ShowBombRadius(playerTransform);
            // 플레이어까지 병의 포물선 운동
            StartCoroutine(ThrowObject(transform.position, playerTransform.position, speed));
        }

        // 폭탄 터지는 범위 효과 보여주기
        private void ShowBombRadius(Transform playerTransform)
        {
            _bombRadiusEffect = Instantiate(_bombRadiusPrefab, playerTransform.position, Quaternion.identity);
            // 폭탄 터지는 범위 효과 크기 조절
            _bombRadiusEffect.transform.localScale = new Vector3(_bombRadius * 2f, 0.001f, _bombRadius * 2f);
            // 폭탄 임펙트 크기 조절
            _bombEffect.transform.localScale = new Vector3(_bombRadius * 0.3f, _bombRadius * 0.3f, _bombRadius * 0.3f);
        }

        // 병이 날아가는 코루틴
        private IEnumerator ThrowObject(Vector3 startPos, Vector3 endPos, float speed)
        {
            float elapsed = 0f;

            while(elapsed < speed)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / speed;

                // 수평 직선 운동
                Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);
                
                // 수직 높이 계산
                // 이차 함수 -4h(t - 0.5)^2 + h
                float verticalOffset = -4 * _height * Mathf.Pow(t - 0.5f, 2) + _height;
                
                // 최종 위치 적용
                transform.position = new Vector3(currentPos.x, currentPos.y + verticalOffset, currentPos.z);

                yield return null;
            }
            
            // 마지막 위치 고정
            transform.position = endPos;
            // 폭탄 터트리기
            Bomb();
        }

        // 병 폭탄이 터지는 메소드
        private void Bomb()
        {
            // 폭탄 터지는 범위 효과 제거
            if(_bombRadiusEffect != null)
            {
                Destroy(_bombRadiusEffect, 0.1f);
            }
            
            // 폭탄 터지는 효과
            _bombEffect.Play();
            
            // 플레이어에게 데미지 주기
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _bombRadius);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    hitCollider.GetComponent<Character.Player.Player>().TakeDamage(20); // 예시 데미지
                }
            }

            // 오브젝트 삭제
            Destroy(gameObject, 1.0f);
        }
    }    
}