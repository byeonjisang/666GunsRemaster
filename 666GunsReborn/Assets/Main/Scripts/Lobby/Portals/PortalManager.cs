using System;
using UnityEngine;

/// <summary>
/// ������ 2025.4.12
/// Action ��������Ʈ�� ������ �����ϴ� Manager
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


    // ���� PortalManager�� �޼���
    public void OnAcceptButtonClicked() => Accept();
    public void OnRejectButtonClicked() => Reject();

}
