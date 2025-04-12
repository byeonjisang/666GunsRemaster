using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ü GameManager�̱� ������ ���� � �������� ����.
public enum GameMode
{
    LOBBY,
    SHOP,
    GAMESTART,
    INGAME
}
public class GameManager : Singleton<GameManager>
{
    //���� ��� ����
    public GameMode _gameMode;

    void Awake()
    {
        //ó�� ���� �� �κ�
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
        Debug.Log("���� ���� ��� : " +  _gameMode);
    }
}
