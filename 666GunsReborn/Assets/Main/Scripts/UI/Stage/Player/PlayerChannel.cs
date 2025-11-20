using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerChannel", menuName = "ScriptableObjects/Channels/PlayerChannel")]
public class PlayerChannel : ScriptableObject
{
    #region UI Button -> Player
    // 대쉬 명령 이벤트
    public event Action OnDashCommand;
    public void SendDashCommand() => OnDashCommand?.Invoke();

    // 공격
    public event Action OnFirePointerDown;
    public event Action OnFirePointerUp;
    public void SendFirePointerDown() => OnFirePointerDown?.Invoke();
    public void SendFirePointerUp() => OnFirePointerUp?.Invoke();

    // 이동
    public event Action<Vector3> OnMoveCommand;
    public void SendMoveCommand(Vector3 direction) => OnMoveCommand?.Invoke(direction);
    #endregion

    #region Player -> UI

    #endregion
}
