using UnityEngine;

public class ShopPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("���� ��Ż ������");
        GameManager.Instance.ChangeGameMode(GameMode.SHOP);
        uiPanel.SetActive(false);

        //����ٰ� ���� �� �ʿ��� �߰� ��� ������ ��
        FadeManager.Instance.FadeIn();
    }

    protected override void OnReject()
    {
        Debug.Log("���� ��Ż ������");
        uiPanel.SetActive(false);
    }
}
