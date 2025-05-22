using UnityEngine;
using UnityEngine.Pool;

public interface IPooledObject
{
    void SetPool(IObjectPool<GameObject> pool);  // 오브젝트 풀 설정
    void ResetObject();  // 오브젝트가 풀로 반환될 때 초기화
    void ReturnToPool();  // 직접 풀로 반환하는 함수 추가
}