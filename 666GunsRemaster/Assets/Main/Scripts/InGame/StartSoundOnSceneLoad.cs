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
        // �� �ε� �̺�Ʈ�� �޼��� ���
        SceneManager.sceneLoaded += OnSceneLoaded;

        foreach (Button button in setButtonList)
        {
            button.onClick.AddListener(() => SoundManager.instance.PlayEffectSound(0));
            //Debug.Log(setButtonList + "��ư �̺�Ʈ ��ϵ�");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�Ǿ��� �� Ư���� ó���� �ʿ��ϴٸ� �߰��� �� ����
        if (scene.name == "TitleScene")
        {
            SoundManager.instance.PlayBGMSound(soundIndex);
        }
    }
}
