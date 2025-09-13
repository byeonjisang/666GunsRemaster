using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private DialogData _dialogData;
    [SerializeField] private GameObject _dialogUI;
    [SerializeField] private Text _text;

    //��ȭâ�� �ؽ�Ʈ ���� �� ���
    public void DialogEvent()
    {
        if(_dialogData.DialogFinished())
        {
            ControlDialogInterface(false);
            _dialogData.ResetDialog();
            return;
        }

        if(!_dialogUI.activeSelf)
        {
            ControlDialogInterface(true);
        }

        _text.text = _dialogData.GetDialogText();
    }

    //��ȭâ UI ����
    private void ControlDialogInterface(bool isTrue)
    {
        _dialogUI.SetActive(isTrue);
    }
}
