using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("���� ���� ��Ż ������");
        GameManager.Instance.ChangeGameMode(GameMode.GAMESTART);
        uiPanel.SetActive(false);

        //����ٰ� ���� �� �ʿ��� �߰� ��� ������ ��
        FadeManager.Instance.FadeAndLoadScene("Stage Select");
    }

    protected override void OnReject()
    {
        Debug.Log("���� ���� ��Ż ������");
        uiPanel.SetActive(false);
    }
}
