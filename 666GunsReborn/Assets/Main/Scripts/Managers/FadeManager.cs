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
    /// DontDestroy �ʿ��� �̱��� �Ŵ������� ������ ȣ��!
    /// </summary>
    protected override bool IsPersistent => true;

    protected override void Awake()
    {
        base.Awake();

        // �ڵ����� canvasGroup ã�ƿ��� (Ȥ�� ���� �ȵ��� �� ���)
        if (fadeCanvasGroup == null)
        {
            Debug.Log("FadeManager ����!");
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

            //���� ��ο�����.
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

        // �� ��ȯ
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // �� ��ȯ �� ��¦ ���

        // �� ��ȯ �� ������ ����
        afterSceneLoad?.Invoke();

        yield return FadeCoroutine(1f, 0f, null);
    }
}
