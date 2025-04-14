using System;
using UnityEngine;

/// <summary>
/// 변지상 2025.4.12
/// Action 델리게이트의 실행을 관리하는 Manager
/// </summary>
public class PortalManager : MonoBehaviour
{
    public Action onAccept;
    public Action onReject;

    public void Accept()
    {
        Debug.Log("PortalManager: Accept called");
        onAccept?.Invoke();
    }

    public void Reject()
    {
        Debug.Log("PortalManager: Reject called");
        onReject?.Invoke();
    }

    public void ClearDelegates()
    {
        onAccept = null;
        onReject = null;
    }


    // 공용 PortalManager의 메서드
    public void OnAcceptButtonClicked() => Accept();
    public void OnRejectButtonClicked() => Reject();

}
