using UnityEngine;
using UnityEngine.UI;

public class ExitView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    // 중재자
    public ExitPresenter Presenter { get; private set; }

    public void Init()
    {
        Presenter = new ExitPresenter(this);

        _exitButton.onClick.AddListener(() =>
        {
            Debug.Log("Exit Button Clicked.");
            Presenter.OnExitRequested?.Invoke();
        });
    }

    public void ExitButtonInteractable(bool interactable)
    {
        _exitButton.interactable = interactable;
    }
}