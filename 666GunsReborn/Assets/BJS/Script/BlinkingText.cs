using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    public Text text;                  // ������ �ؽ�Ʈ
    public float blinkInterval = 0.5f; // �ؽ�Ʈ �����̴� ����
    public CanvasGroup[] objectsToFadeOut; // ���̵� �ƿ��� ������Ʈ��
    public CanvasGroup[] objectsSetActive; // Ȱ��ȭ�� ������Ʈ��
    public float fadeDuration = 1.0f;  // ���̵� �ƿ��� �ɸ��� �ð�

    private void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            text.enabled = !text.enabled; // �ؽ�Ʈ Ȱ��ȭ/��Ȱ��ȭ ��ȯ
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
