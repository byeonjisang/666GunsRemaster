using UnityEngine;


/// <summary>
/// 변지상 2025.4.12
/// 여러 개의 포탈 제작 시 사용하게 될 Base 클래스.
/// Action 델리게이트를 여기서 관리하게 된다.
/// </summary>
public abstract class PortalBase : MonoBehaviour
{
    [SerializeField] protected PortalManager portalManager;
    [SerializeField] protected GameObject uiPanel;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        portalManager.ClearDelegates();  //중복 방지
        portalManager.onAccept += OnAccept;
        portalManager.onReject += OnReject;

        uiPanel.SetActive(true);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        portalManager.ClearDelegates();  //빠져나가면 등록 해제
        uiPanel.SetActive(false);
    }

    protected abstract void OnAccept();
    protected abstract void OnReject();
}
