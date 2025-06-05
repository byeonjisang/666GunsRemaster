using UnityEngine;

public class ShopPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("상점 포탈 수락됨");
        GameManager.Instance.ChangeGameMode(GameMode.SHOP);
        uiPanel.SetActive(false);

        //여기다가 수락 시 필요한 추가 기능 넣으면 됨
        FadeManager.Instance.FadeAndLoadScene("CostumePurchaseScene");
    }

    protected override void OnReject()
    {
        Debug.Log("상점 포탈 거절됨");
        uiPanel.SetActive(false);
    }
}
