using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private PlayerChannel playerChannel;

    [Header("Views")]
    [SerializeField] private FireButtonView fireButtonView;
    [SerializeField] private DashButtonView dashButtonView;
    [SerializeField] private MoveJoystickView moveJoystickView;

    // presenter
    private PlayerPresenter _playerPresenter;

    private void Start()
    {
        _playerPresenter = new PlayerPresenter(
            playerChannel,
            fireButtonView,
            dashButtonView,
            moveJoystickView
        );
    }

    private void OnDestroy()
    {
        _playerPresenter.Dispose();
    }
}
