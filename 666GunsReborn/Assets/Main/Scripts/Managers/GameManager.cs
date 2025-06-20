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
    public int _coin = 0;

    public Button _settingButton;

    protected override void Awake()
    {
        base.Awake();

        //ó�� ���� �� �κ�
        _gameMode = GameMode.LOBBY;

        _coin = 1000;
    }

    protected void Update()
    {
        ShowCoinText();
    }

    public void ShowCoinText()
    {
        //_userCoinText.text = _coin.ToString() + " Coins";
    }

    public void SetCoinTextTarget(Text coinText)
    {
        _userCoinText = coinText;
        ShowCoinText();
    }


    public void ChangeGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        Debug.Log("���� ���� ��� : " +  _gameMode);
    }
}
