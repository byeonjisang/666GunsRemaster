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

        //������ ���̵� ������ �ƿ���Ű�� ����
        FadeOut();

        // �ڵ����� canvasGroup ã�ƿ��� (Ȥ�� ���� �ȵ��� �� ���)
        if (fadeCanvasGroup == null)
        {
            Debug.Log("FadeManager ����!");
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

        // �� �񵿱� �ε�
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

        //���̵� ĵ������ �Է��� ���´�
        fadeCanvasGroup.blocksRaycasts = true;
        fadeCanvasGroup.interactable = false;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;

        // ���̵尡 �Ϸ�Ǹ� �Է� ��� ���� ����
        fadeCanvasGroup.blocksRaycasts = endAlpha != 0;
        fadeCanvasGroup.interactable = false; // ���� ���̵�� Ŭ�� ����� �ƴϴϱ�
    }
}
