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
        _gameMode = GameMode.LOBBY;
        _coin = 1000;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var coinObj = GameObject.Find("PlayerCoinText");
        if (coinObj != null)
        {
            _userCoinText = coinObj;
            _text = _userCoinText.GetComponent<Text>();
            Debug.Log($"[GameManager] UserCoinText initialized in scene {scene.name}");
        }
        else
        {
            _userCoinText = null;
            _text = null;
            Debug.LogWarning($"[GameManager] No PlayerCoinText in scene {scene.name}");
        }
    }

    //IEnumerator InitCoinText()
    //{
    //    yield return null; // 한 프레임 대기

    //    _userCoinText = GameObject.Find("PlayerCoinText");
    //    if (_userCoinText != null)
    //    {
    //        _text = _userCoinText.GetComponent<Text>();
    //        Debug.Log("CoinText found and initialized!");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("PlayerCoinText not found in scene!");
    //    }
    //}


    protected void Update()
    {
        if(_userCoinText != null) 
        {
            ShowCoinText();
        }
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
