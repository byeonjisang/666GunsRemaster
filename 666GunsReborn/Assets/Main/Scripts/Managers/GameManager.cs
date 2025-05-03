using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;


//��ü GameManager�̱� ������ ���� � �������� ����.
public enum GameMode
{
    LOBBY,
    SHOP,
    WEAPONTEST,
    GAMESTART,
    INGAME
}
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// DontDestroy �ʿ��� �̱��� �Ŵ������� ������ ȣ��!
    /// </summary>
    protected override bool IsPersistent => true;

    //���� ��� ����
    public GameMode _gameMode;

    //�÷��̾� ���� ��ȭ ǥ��
    public Text _userCoinText;

    public Button _settingButton;

    public Action _onAccept;
    public Action _onReject;

    protected override void Awake()
    {
        base.Awake();

        //ó�� ���� �� �κ�
        _gameMode = GameMode.LOBBY;
    }
    public void ChangeGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        Debug.Log("���� ���� ��� : " +  _gameMode);
    }

    public void Accept()
    {
        Debug.Log("PortalManager: Accept called");
        _onAccept?.Invoke();
    }

    public void Reject()
    {
        Debug.Log("PortalManager: Reject called");
        _onReject?.Invoke();
    }

    public void ClearDelegates()
    {
        _onAccept = null;
        _onReject = null;
    }

    /// <summary>
    /// �κ� �� ���� ���������� ��ư �̺�Ʈ�� ���� GameManager�� Action�� ���� ȣ���Ѵ�.
    /// </summary>
    public void OnAcceptButtonClicked() => Accept();
    public void OnRejectButtonClicked() => Reject();
}
