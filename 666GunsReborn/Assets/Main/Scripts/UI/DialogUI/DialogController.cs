using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private DialogData _dialogData;
    [SerializeField] private GameObject _dialogUI;

    private void ControlDialogInterface(bool isTrue)
    {
        _dialogUI.SetActive(isTrue);
    }
}
