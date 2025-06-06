using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeInExit : MonoBehaviour
{
    public GameObject _questionUI;
    public void ExitToOtherScene()
    {
        FadeManager.Instance.FadeAndLoadScene("Lobby");
    }

    public void SetActiveQuestionUI()
    {
        _questionUI.SetActive(true);
    }
    public void RejectQuestionUI()
    {
        _questionUI.SetActive(false);
    }
}
