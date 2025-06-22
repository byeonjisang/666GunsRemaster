using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Blink : MonoBehaviour
{
    public Text text;                  // 깜빡일 텍스트
    public float blinkInterval = 0.5f; // 텍스트 깜빡이는 간격

    private void Start()
    {
        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            text.enabled = !text.enabled; // 텍스트 활성화/비활성화 전환
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
