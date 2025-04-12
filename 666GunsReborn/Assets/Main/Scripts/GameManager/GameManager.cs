using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전체 GameManager이기 떄문에 현재 어떤 상태인지 구분.
public enum GameMode
{
    LOBBY,
    SHOP,
    GAMESTART,
    INGAME
}
public class GameManager : Singleton<GameManager>
{
    //게임 모드 변경
    public GameMode _gameMode;

    void Awake()
    {
        //처음 입장 시 로비
        _gameMode = GameMode.LOBBY;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        Debug.Log("현재 게임 모드 : " +  _gameMode);
    }
}
