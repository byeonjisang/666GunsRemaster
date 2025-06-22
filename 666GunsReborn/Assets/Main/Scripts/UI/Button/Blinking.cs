using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour
{
    public Text text;                  // ������ �ؽ�Ʈ
    public float blinkInterval = 0.5f; // �ؽ�Ʈ �����̴� ����

    private void Start()
    {
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            text.enabled = !text.enabled; // �ؽ�Ʈ Ȱ��ȭ/��Ȱ��ȭ ��ȯ
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
