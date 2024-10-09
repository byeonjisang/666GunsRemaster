using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSoundOnSceneLoad : MonoBehaviour
{
    public int soundIndex = 0;

    [SerializeField] private List<Button> setButtonList = new List<Button>();

    void Start()
    {
        // 씬 로드 이벤트에 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;

        foreach (Button button in setButtonList)
        {
            button.onClick.AddListener(() => SoundManager.instance.PlayEffectSound(0));
            //Debug.Log(setButtonList + "버튼 이벤트 등록됨");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되었을 때 특별한 처리가 필요하다면 추가할 수 있음
        if (scene.name == "TitleScene")
        {
            SoundManager.instance.PlayBGMSound(soundIndex);
        }
    }
}
