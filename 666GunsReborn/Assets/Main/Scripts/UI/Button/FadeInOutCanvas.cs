using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutCanvas : MonoBehaviour
{
    public float _fadeDuration = 0.5f;

    public CanvasGroup _currentGroup;
    public CanvasGroup _nextGroup;

    public void FadeToNextCanvas()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        yield return StartCoroutine(FadeCanvas(_currentGroup, 1f, 0f));
        _currentGroup.gameObject.SetActive(false);

        _nextGroup.gameObject.SetActive(true);
        yield return StartCoroutine(FadeCanvas(_nextGroup, 0f, 1f));
    }

    private IEnumerator FadeCanvas(CanvasGroup group, float startAlpha, float endAlpha)
    {
        group.alpha = startAlpha;
        group.interactable = false;
        group.blocksRaycasts = false;

        float elapsed = 0f;
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed);
            yield return null;
        }

        group.alpha = endAlpha;
        group.interactable = (endAlpha > 0.9f);
        group.blocksRaycasts = (endAlpha > 0.9f);
    }
}
