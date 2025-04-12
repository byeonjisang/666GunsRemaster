using UnityEngine;


/// <summary>
/// ������ 2025.4.12
/// ���� ���� ��Ż ���� �� ����ϰ� �� Base Ŭ����.
/// Action ��������Ʈ�� ���⼭ �����ϰ� �ȴ�.
/// </summary>
public abstract class PortalBase : MonoBehaviour
{
    [SerializeField] protected PortalManager portalManager;
    [SerializeField] protected GameObject uiPanel;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        portalManager.ClearDelegates();  //�ߺ� ����
        portalManager.onAccept += OnAccept;
        portalManager.onReject += OnReject;

        uiPanel.SetActive(true);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        portalManager.ClearDelegates();  //���������� ��� ����
        uiPanel.SetActive(false);
    }

    protected abstract void OnAccept();
    protected abstract void OnReject();
}
