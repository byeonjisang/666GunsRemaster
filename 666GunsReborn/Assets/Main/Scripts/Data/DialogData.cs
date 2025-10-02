using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogData : MonoBehaviour
{
    private int _dialogStep = 0;
    public string[] _dialogText;

    public string GetDialogText()
    {
        if(_dialogText.Length <= _dialogStep)
        {
            return "....";
        }

        return _dialogText[_dialogStep++];
    }

    public void ResetDialog()
    {
        _dialogStep = 0;
    }

    public bool DialogFinished()
    {
        return _dialogStep >= _dialogText.Length;
    }
}
