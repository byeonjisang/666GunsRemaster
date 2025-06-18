using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;


//전체 GameManager이기 떄문에 현재 어떤 상태인지 구분.
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
    /// DontDestroy 필요한 싱글턴 매니저들은 무조건 호출!
    /// </summary>
    protected override bool IsPersistent => true;

    //게임 모드 변경
    public GameMode _gameMode;

    //플레이어 코인 재화 표시
    public Text _userCoinText;
    public int _coin = 0;

    public Button _settingButton;

    protected override void Awake()
    {
        base.Awake();

        //처음 입장 시 로비
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
        Debug.Log("현재 게임 모드 : " +  _gameMode);
    }
}
