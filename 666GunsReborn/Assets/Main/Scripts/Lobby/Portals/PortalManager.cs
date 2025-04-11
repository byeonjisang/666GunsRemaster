using System;
using UnityEngine;

/// <summary>
/// ��� ��Ż�� �� ��ũ��Ʈ�� ����� �����Ѵ�.
/// </summary>
/// 

public class PortalManager : MonoBehaviour
{
    //��������Ʈ�� ����
    public Action onAccept;
    public Action onReject;

    public void Accept()
    {
        onAccept?.Invoke();

    }

    public void Reject()
    {
        onReject?.Invoke();
    }
}
