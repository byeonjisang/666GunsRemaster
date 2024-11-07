using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    public Text text;                  // 깜빡일 텍스트
    public float blinkInterval = 0.5f; // 텍스트 깜빡이는 간격
    public CanvasGroup[] objectsToFadeOut; // 페이드 아웃할 오브젝트들
    public CanvasGroup[] objectsSetActive; // 활성화할 오브젝트들
    public float fadeDuration = 1.0f;  // 페이드 아웃에 걸리는 시간

    private void Start()
    {
        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 터치 또는 마우스 클릭
        {
            StartCoroutine(FadeOutObjects());
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            text.enabled = !text.enabled; // 텍스트 활성화/비활성화 전환
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private IEnumerator FadeOutObjects()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            foreach (var obj in objectsToFadeOut)
            {
                if (obj != null)
                {
                    obj.alpha = 1 - (elapsedTime / fadeDuration); // 알파 값 감소
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 페이드 아웃 후 오브젝트들을 비활성화
        foreach (var obj in objectsToFadeOut)
        {
            if (obj != null)
            {
                obj.alpha = 0;
                obj.gameObject.SetActive(false);
            }
        }
        foreach (var obj in objectsSetActive)
        {
            if (obj != null)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
