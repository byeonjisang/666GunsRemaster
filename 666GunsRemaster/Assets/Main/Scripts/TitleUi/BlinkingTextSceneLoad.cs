using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlinkingTextSceneLoad : MonoBehaviour
{
    public Text text;                  // ������ �ؽ�Ʈ
    public float blinkInterval = 0.5f; // �ؽ�Ʈ �����̴� ����
    public CanvasGroup[] objectsToFadeOut; // ���̵� �ƿ��� ������Ʈ��
    public string loadSceneName;    //�̵��� �� �̸�
    public float fadeDuration = 1.0f;  // ���̵� �ƿ��� �ɸ��� �ð�

    private void Start()
    {
        StartCoroutine(Blink());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��ġ �Ǵ� ���콺 Ŭ��
        {
            StartCoroutine(FadeOutObjects());
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            text.enabled = !text.enabled; // �ؽ�Ʈ Ȱ��ȭ/��Ȱ��ȭ ��ȯ
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
                    obj.alpha = 1 - (elapsedTime / fadeDuration); // ���� �� ����
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���̵� �ƿ� �� ������Ʈ���� ��Ȱ��ȭ
        foreach (var obj in objectsToFadeOut)
        {
            if (obj != null)
            {
                obj.alpha = 0;
                obj.gameObject.SetActive(false);
                //�� ��ȯ
                SoundManager.instance.StopAllSound();
                SceneManager.LoadScene(loadSceneName);
            }
        }
    }
}
