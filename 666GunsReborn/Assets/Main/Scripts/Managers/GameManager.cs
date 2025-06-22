using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject _userCoinText;
    private Text _text;
    public int _coin = 0;

    public Button _settingButton;

    protected override void Awake()
    {
        base.Awake();

        //ó�� ���� �� �κ�
        _gameMode = GameMode.LOBBY;

        _coin = 1000;

        _text = _userCoinText.GetComponent<Text>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _userCoinText = GameObject.Find("PlayerCoinText");
    }

    protected void Update()
    {
        ShowCoinText();
    }

    public void ShowCoinText()
    {
        ///인게임에서는 재화 표시 X
        if( _gameMode == GameMode.INGAME)
        {
            return;
        }
        else 
        {
            _text.text = _coin.ToString() + " Coins";
        }
    }

    public void SetCoinTextTarget(GameObject coinText)
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
