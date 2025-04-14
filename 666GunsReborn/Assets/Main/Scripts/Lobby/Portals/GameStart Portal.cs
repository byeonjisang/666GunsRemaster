using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("게임 시작 포탈 수락됨");
        GameManager.Instance.ChangeGameMode(GameMode.GAMESTART);
        uiPanel.SetActive(false);

        //여기다가 수락 시 필요한 추가 기능 넣으면 됨
        FadeManager.Instance.FadeOut();
    }

    protected override void OnReject()
    {
        Debug.Log("게임 시작 포탈 거절됨");
        uiPanel.SetActive(false);
    }
}
