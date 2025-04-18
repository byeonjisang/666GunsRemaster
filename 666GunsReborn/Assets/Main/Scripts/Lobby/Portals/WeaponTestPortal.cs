using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ 2025.4.12
/// �÷��̾ �ѱ� ���������� ���� ���� ��Ż
/// </summary>
public class WeaponTestPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("���� �׽�Ʈ�� ��Ż ������");
        GameManager.Instance.ChangeGameMode(GameMode.WEAPONTEST);
        uiPanel.SetActive(false);

        //����ٰ� ���� �� �ʿ��� �߰� ��� ������ ��
        FadeManager.Instance.FadeToScene("WeaponTestScene");
    }

    protected override void OnReject()
    {
        Debug.Log("���� �׽�Ʈ�� ��Ż ������");
        uiPanel.SetActive(false);
    }
}
