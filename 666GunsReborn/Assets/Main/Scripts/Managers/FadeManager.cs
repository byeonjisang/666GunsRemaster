using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FadeManager;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    /// <summary>
    /// DontDestroy 필요한 싱글턴 매니저들은 무조건 호출!
    /// </summary>
    protected override bool IsPersistent => true;

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
        StartCoroutine(FadeCoroutine(0f, 1f, onComplete));
    }

    public void FadeOut(System.Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(1f, 0f, onComplete));
    }

    public void FadeToScene(string sceneName, System.Action afterSceneLoad = null)
    {
        StartCoroutine(FadeToSceneRoutine(sceneName, afterSceneLoad));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, System.Action onComplete)
    {
        float time = 0f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            //점점 어두워진다.
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = true;

        onComplete?.Invoke();
    }

    private IEnumerator FadeToSceneRoutine(string sceneName, System.Action afterSceneLoad)
    {
        yield return FadeCoroutine(0f, 1f, null);

        // 씬 전환
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // 씬 전환 후 살짝 대기

        // 씬 전환 후 실행할 동작
        afterSceneLoad?.Invoke();

        yield return FadeCoroutine(1f, 0f, null);
    }
}
