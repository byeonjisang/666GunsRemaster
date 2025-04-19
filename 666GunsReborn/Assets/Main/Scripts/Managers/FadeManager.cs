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

        //가려진 페이드 무조건 아웃시키고 시작
        FadeOut();

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

        //페이드 캔버스만 입력을 막는다
        fadeCanvasGroup.blocksRaycasts = true;
        fadeCanvasGroup.interactable = false;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;

        // 페이드가 완료되면 입력 허용 여부 설정
        fadeCanvasGroup.blocksRaycasts = endAlpha != 0;
        fadeCanvasGroup.interactable = false; // 보통 페이드는 클릭 대상이 아니니까
    }
}
