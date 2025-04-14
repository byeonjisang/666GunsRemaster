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

        //�� ��ȯ�� �� ���� ����ϱ⿡ �ı��Ǹ� �� ��.
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

            //���� ��ο�����.
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
