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
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�Ǿ��� �� Ư���� ó���� �ʿ��ϴٸ� �߰��� �� ����
        if (scene.name == "TitleScene")
        {
            SoundManager.instance.PlayBGMSound(soundIndex);

            for (int i = 0; i < setButtonList.Count; i++)
            {
                setButtonList[i].onClick.AddListener(() => SoundManager.instance.PlayEffectSound(0));
                Debug.Log("��ư �̺�Ʈ ��ϵ�");
            }
        }
    }
}
