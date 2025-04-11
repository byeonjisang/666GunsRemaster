using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameStartPortal : MonoBehaviour
{
    public PortalManager _portalManager;

    public GameObject _gameStartUI;

    void Start()
    {
        //버튼 클릭 메서드 실행
        _portalManager.onAccept = () => {
            GameManager.Instance.ChangeGameMode(GameMode.GAMESTART);
            _gameStartUI.SetActive(false);

            //게임 씬 전환 or 스테이지 전환 시 기능 여기다가 추가하면 됨
        };

        _portalManager.onReject = () => {
            _gameStartUI.SetActive(false);
        };

    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameStartUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameStartUI.SetActive(false);
        }
    }

    public void GoToGame()
    {
        _portalManager.Accept();
    }

    public void DontGoToGame()
    {
        _portalManager.Reject();
    }
}
