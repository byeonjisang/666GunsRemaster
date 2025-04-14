using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : Singleton<FadeManager>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        //씬 전환할 때 마다 사용하기에 파괴되면 안 됨.
        DontDestroyOnLoad(gameObject);
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

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            //점점 어두워진다.
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.blocksRaycasts = false;

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
