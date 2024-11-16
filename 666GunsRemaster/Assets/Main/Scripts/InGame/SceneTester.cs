using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Button을 사용하기 위한 네임스페이스

public class SceneTester : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Button sceneChangeButton; // Button 연결

    [SerializeField] 
    private string loadSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Button의 OnClick 이벤트에 SceneChange 메서드를 연결
        sceneChangeButton.onClick.AddListener(SceneChange);
        canvasGroup.alpha = 1f; // alpha 값을 정확히 1로 설정
    }

    public void SceneChange()
    {
        StartCoroutine(FadeCanvasGroup());
    }

    IEnumerator FadeCanvasGroup()
    {
        float time = 0f;
        float duration = 1f; // alpha 변화가 일어나는 시간

        while (time <= duration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration); // 0에서 1까지 alpha 값 변화
            yield return null; // 한 프레임 대기
        }

        //씬 바뀔 때 모든 소리 다 중지
        //SoundManager.instance.StopAllSound();
        LoadScene.LoadGameScene(loadSceneName); // alpha가 1이 되었을 때 씬 변경
    }
}