using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MonsterScannerTest : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private LineRenderer lineRenderer;

    void Awake()
    {
        // LineRenderer 컴포넌트 추가
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f; // 기본 선의 두께
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 재질

        // 그라데이션 설정
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, // 색상 변화
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) } // 알파 값
        );
        lineRenderer.colorGradient = gradient; // 그라데이션 적용
        lineRenderer.positionCount = 0; // 초기에는 선이 없음
    }

    void FixedUpdate()
    {
        //CircleCast 생성
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        //가장 가까운 오브젝트의 위치 자동 조준
        nearestTarget = GetNearestTarget();

        DrawTargetLines();
    }

    Transform GetNearestTarget()
    {
        Transform res = null;
        float diff = 10f;

        foreach (RaycastHit2D ray in targets)
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = ray.transform.position;

            float dis = Vector3.Distance(pos, targetPos);

            //거리가 더 작은 오브젝트로 교체
            if (dis < diff)
            {
                diff = dis;
                res = ray.transform;
                Debug.Log(res.gameObject.name);
            }
        }

        return res;
    }

    // CircleCast 범위를 시각적으로 확인할 수 있도록 Gizmos 사용
    void OnDrawGizmos()
    {
        // CircleCast 탐색 범위 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);

        // 탐색된 타겟들을 작은 구체로 표시
        if (targets != null)
        {
            foreach (RaycastHit2D target in targets)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(target.transform.position, 0.2f); // 탐색된 타겟 위치에 구체 그리기
            }
        }

        // 가장 가까운 타겟을 노란색으로 표시
        if (nearestTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(nearestTarget.position, 0.3f); // 가장 가까운 타겟은 더 큰 구체로 표시
        }
    }

    void DrawTargetLines()
    {
        int totalPoints = (targets != null ? targets.Length : 0) + (nearestTarget != null ? 1 : 0);

        if (totalPoints > 0)
        {
            lineRenderer.positionCount = totalPoints * 2; // 각 타겟에 두 개의 포지션 필요 (출발지와 도착지)

            int index = 0;
            foreach (RaycastHit2D target in targets)
            {
                // 모든 타겟에 대해 초록색 선 그리기 (그라데이션의 첫 번째 부분 사용)
                lineRenderer.SetPosition(index, transform.position);  // 출발지 (플레이어)
                lineRenderer.SetPosition(index + 1, target.transform.position); // 도착지 (타겟)
                index += 2;
            }

            // 가장 가까운 타겟에 노란색 선 그리기 (그라데이션의 마지막 부분 사용)
            if (nearestTarget != null)
            {
                lineRenderer.SetPosition(index, transform.position);  // 출발지 (플레이어)
                lineRenderer.SetPosition(index + 1, nearestTarget.position); // 도착지 (가장 가까운 타겟)
            }
        }
        else
        {
            // 타겟이 없을 경우, 선을 그리지 않음
            lineRenderer.positionCount = 0;
        }
    }
}
