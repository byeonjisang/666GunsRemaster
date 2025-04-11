using System;
using UnityEngine;

/// <summary>
/// 모든 포탈은 이 스크립트의 기능을 공유한다.
/// </summary>
/// 

public class PortalManager : MonoBehaviour
{
    //델리게이트로 관리
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
