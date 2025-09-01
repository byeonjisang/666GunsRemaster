using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeInOtherScene : MonoBehaviour
{
    public GameObject _canvas;
    public string _sceneName;
    public void ExitToOtherScene()
    {
        FadeManager.Instance.FadeAndLoadScene(_sceneName);
    }

    public void SetActiveQuestionUI()
    {
        _canvas.SetActive(true);
    }
    public void RejectQuestionUI()
    {
        _canvas.SetActive(false);
    }
}
