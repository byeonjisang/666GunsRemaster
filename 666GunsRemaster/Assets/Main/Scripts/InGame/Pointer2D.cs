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

        // Ȱ��ȭ�� Ÿ�� ã��
        currentTarget = GetActiveTarget();

        // Ȱ��ȭ�� Ÿ���� ������ ȭ��ǥ ����
        if (currentTarget == null)
        {
            arrowUI.gameObject.SetActive(false);
            return;
        }

        // ��ǥ�� ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(currentTarget.position);

        // ȭ�� �ȿ� �ִ��� Ȯ��
        bool isOnScreen = screenPosition.z > 0 &&
                          screenPosition.x > 0 && screenPosition.x < Screen.width &&
                          screenPosition.y > 0 && screenPosition.y < Screen.height;

        // ȭ�� �߽� ��ǥ ���
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

        // ȭ�� �߽ɿ��� ��ǥ ��ġ���� ���� ���
        Vector3 direction = screenPosition - screenCenter;

        // ������ ���� ���ͷ� ��ȯ
        direction.Normalize();

        // ȭ��ǥ�� ��ǥ ������ ����Ű���� ȸ��
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // ȭ��ǥ ǥ��
        arrowUI.gameObject.SetActive(true);

        //if (!isOnScreen)
        //{
           
        //}
        //else
        //{
        //    // ��ǥ�� ȭ�� �ȿ� ���� �� ȭ��ǥ ����
        //    //arrowUI.gameObject.SetActive(false);
        //}
    }

    //Ȱ��ȭ�� Ÿ���� ã�� �޼���
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
