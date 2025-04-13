using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button fireButton;
    [SerializeField]
    private Button dashButton;

    private void Start()
    {
        //fireButton.onClick.AddListener(() => PlayerController.Instance.Attack());
        dashButton.onClick.AddListener(() => PlayerController.Instance.Dash());
    }
}