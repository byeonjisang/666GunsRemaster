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
        //��ư Ŭ�� �޼��� ����
        _portalManager.onAccept = () => {
            GameManager.Instance.ChangeGameMode(GameMode.GAMESTART);
            _gameStartUI.SetActive(false);

            //���� �� ��ȯ or �������� ��ȯ �� ��� ����ٰ� �߰��ϸ� ��
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
