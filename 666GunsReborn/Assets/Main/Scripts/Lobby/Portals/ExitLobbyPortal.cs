using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ 2025.4.12
/// �÷��̾ �κ�� ���� ���� ��Ż
/// </summary>
public class ExitLobbyPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("���� �׽�Ʈ�� ��Ż ������");
        GameManager.Instance.ChangeGameMode(GameMode.LOBBY);
        uiPanel.SetActive(false);

        //����ٰ� ���� �� �ʿ��� �߰� ��� ������ ��
        FadeManager.Instance.FadeAndLoadScene("Lobby");
    }

    protected override void OnReject()
    {
        Debug.Log("���� �׽�Ʈ�� ��Ż ������");
        uiPanel.SetActive(false);
    }
}
