using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResolutionAdjuster : MonoBehaviour
{
    public Vector2 referenceResolution = new Vector2(1920, 1080);  // 기준 해상도 (예: 1920x1080)

    void Start()
    {
        AdjustResolution();
    }

    void AdjustResolution()
    {
        // 현재 화면 비율
        float screenRatio = (float)Screen.width / (float)Screen.height;

        // 기준 해상도 비율
        float referenceRatio = referenceResolution.x / referenceResolution.y;

        // 화면 비율에 맞춰서 카메라의 orthographicSize 또는 Viewport 설정
        Camera.main.orthographicSize = referenceResolution.y / 200f;

        // 화면 비율이 기준 비율보다 크면(와이드), 너비를 맞추고 높이를 조정
        if (screenRatio >= referenceRatio)
        {
            Camera.main.orthographicSize *= screenRatio / referenceRatio;
        }
    }
}