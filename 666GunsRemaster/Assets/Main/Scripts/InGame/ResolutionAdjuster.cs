using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResolutionAdjuster : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(1920, 1080);  // ���� �ػ� (��: 1920x1080)

    void Start()
    {
        AdjustResolution();
    }

    void AdjustResolution()
    {
        // ���� ȭ�� ����
        float screenRatio = (float)Screen.width / (float)Screen.height;

        // ���� �ػ� ����
        float referenceRatio = referenceResolution.x / referenceResolution.y;

        // ȭ�� ������ ���缭 ī�޶��� orthographicSize �Ǵ� Viewport ����
        Camera.main.orthographicSize = referenceResolution.y / 200f;

        // ȭ�� ������ ���� �������� ũ��(���̵�), �ʺ� ���߰� ���̸� ����
        if (screenRatio >= referenceRatio)
        {
            Camera.main.orthographicSize *= screenRatio / referenceRatio;
        }
    }
}