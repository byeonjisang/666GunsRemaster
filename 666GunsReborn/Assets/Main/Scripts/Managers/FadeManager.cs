using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FadeManager;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    protected override void Awake()
    {
        base.Awake();

        // 자동으로 canvasGroup 찾아오기 (혹시 연결 안됐을 때 대비)
        if (fadeCanvasGroup == null)
        {
            Debug.Log("FadeManager 없음!");
        }
    }

    public void FadeIn(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(0f, 1f));
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(1f, 0f));
    }

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(FadeCoroutine(0, 1));

        // 씬 비동기 로딩
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeCoroutine(1, 0));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
    {
        float time = 0f;
        canvasGroup.blocksRaycasts = true;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
        canvasGroup.blocksRaycasts = endAlpha != 0;
    }
}
