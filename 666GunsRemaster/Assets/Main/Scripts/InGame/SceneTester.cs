using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Button�� ����ϱ� ���� ���ӽ����̽�

public class SceneTester : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Button sceneChangeButton; // Button ����

    [SerializeField] 
    private string loadSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Button�� OnClick �̺�Ʈ�� SceneChange �޼��带 ����
        sceneChangeButton.onClick.AddListener(SceneChange);
        canvasGroup.alpha = 1f; // alpha ���� ��Ȯ�� 1�� ����
    }

    public void SceneChange()
    {
        StartCoroutine(FadeCanvasGroup());
    }

    IEnumerator FadeCanvasGroup()
    {
        float time = 0f;
        float duration = 1f; // alpha ��ȭ�� �Ͼ�� �ð�

        while (time <= duration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration); // 0���� 1���� alpha �� ��ȭ
            yield return null; // �� ������ ���
        }

        //�� �ٲ� �� ��� �Ҹ� �� ����
        //SoundManager.instance.StopAllSound();
        LoadScene.LoadGameScene(loadSceneName); // alpha�� 1�� �Ǿ��� �� �� ����
    }
}