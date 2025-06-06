using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 변지상 2025.4.12
/// 플레이어가 로비로 가기 위한 포탈
/// </summary>
public class ExitLobbyPortal : PortalBase
{
    protected override void OnAccept()
    {
        Debug.Log("무기 테스트장 포탈 수락됨");
        GameManager.Instance.ChangeGameMode(GameMode.LOBBY);
        uiPanel.SetActive(false);

        //여기다가 수락 시 필요한 추가 기능 넣으면 됨
        FadeManager.Instance.FadeAndLoadScene("Lobby");
    }

    protected override void OnReject()
    {
        Debug.Log("무기 테스트장 포탈 거절됨");
        uiPanel.SetActive(false);
    }
}
