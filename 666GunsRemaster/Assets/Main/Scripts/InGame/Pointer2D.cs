using System.Collections.Generic;
using UnityEngine;

public class Pointer2D : MonoBehaviour
{
    public List<Transform> targets;
    public RectTransform arrowUI;
    public Camera mainCamera;

    private Transform currentTarget;

    void Update()
    {
        if (arrowUI == null || mainCamera == null || targets == null || targets.Count == 0)
            return;

        // 활성화된 타겟 찾기
        currentTarget = GetActiveTarget();

        // 활성화된 타겟이 없으면 화살표 숨김
        if (currentTarget == null)
        {
            arrowUI.gameObject.SetActive(false);
            return;
        }

        // 목표의 월드 좌표를 화면 좌표로 변환
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(currentTarget.position);

        // 화면 안에 있는지 확인
        bool isOnScreen = screenPosition.z > 0 &&
                          screenPosition.x > 0 && screenPosition.x < Screen.width &&
                          screenPosition.y > 0 && screenPosition.y < Screen.height;

        // 화면 중심 좌표 계산
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        // 화면 중심에서 목표 위치로의 방향 계산
        Vector3 direction = screenPosition - screenCenter;

        // 방향을 단위 벡터로 변환
        direction.Normalize();

        // 화살표가 목표 방향을 가리키도록 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // 화살표 표시
        arrowUI.gameObject.SetActive(true);

        //if (!isOnScreen)
        //{
           
        //}
        //else
        //{
        //    // 목표가 화면 안에 있을 때 화살표 숨김
        //    //arrowUI.gameObject.SetActive(false);
        //}
    }

    //활성화된 타겟을 찾는 메서드
    private Transform GetActiveTarget()
    {
        foreach (var target in targets)
        {
            if (target != null && target.gameObject.activeSelf)
            {
                return target;
            }
        }
        return null;
    }
}
