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
        // LineRenderer ������Ʈ �߰�
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f; // �⺻ ���� �β�
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ����

        // �׶��̼� ����
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.yellow, 1.0f) }, // ���� ��ȭ
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) } // ���� ��
        );
        lineRenderer.colorGradient = gradient; // �׶��̼� ����
        lineRenderer.positionCount = 0; // �ʱ⿡�� ���� ����
    }

    void FixedUpdate()
    {
        //CircleCast ����
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        //���� ����� ������Ʈ�� ��ġ �ڵ� ����
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

            //�Ÿ��� �� ���� ������Ʈ�� ��ü
            if (dis < diff)
            {
                diff = dis;
                res = ray.transform;
                Debug.Log(res.gameObject.name);
            }
        }

        return res;
    }

    // CircleCast ������ �ð������� Ȯ���� �� �ֵ��� Gizmos ���
    void OnDrawGizmos()
    {
        // CircleCast Ž�� ���� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);

        // Ž���� Ÿ�ٵ��� ���� ��ü�� ǥ��
        if (targets != null)
        {
            foreach (RaycastHit2D target in targets)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(target.transform.position, 0.2f); // Ž���� Ÿ�� ��ġ�� ��ü �׸���
            }
        }

        // ���� ����� Ÿ���� ��������� ǥ��
        if (nearestTarget != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(nearestTarget.position, 0.3f); // ���� ����� Ÿ���� �� ū ��ü�� ǥ��
        }
    }

    void DrawTargetLines()
    {
        int totalPoints = (targets != null ? targets.Length : 0) + (nearestTarget != null ? 1 : 0);

        if (totalPoints > 0)
        {
            lineRenderer.positionCount = totalPoints * 2; // �� Ÿ�ٿ� �� ���� ������ �ʿ� (������� ������)

            int index = 0;
            foreach (RaycastHit2D target in targets)
            {
                // ��� Ÿ�ٿ� ���� �ʷϻ� �� �׸��� (�׶��̼��� ù ��° �κ� ���)
                lineRenderer.SetPosition(index, transform.position);  // ����� (�÷��̾�)
                lineRenderer.SetPosition(index + 1, target.transform.position); // ������ (Ÿ��)
                index += 2;
            }

            // ���� ����� Ÿ�ٿ� ����� �� �׸��� (�׶��̼��� ������ �κ� ���)
            if (nearestTarget != null)
            {
                lineRenderer.SetPosition(index, transform.position);  // ����� (�÷��̾�)
                lineRenderer.SetPosition(index + 1, nearestTarget.position); // ������ (���� ����� Ÿ��)
            }
        }
        else
        {
            // Ÿ���� ���� ���, ���� �׸��� ����
            lineRenderer.positionCount = 0;
        }
    }
}
