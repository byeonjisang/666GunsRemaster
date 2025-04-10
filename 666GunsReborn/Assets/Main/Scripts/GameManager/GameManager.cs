using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ü GameManager�̱� ������ ���� � �������� ����.
public enum GameMode
{
    LOBBY,
    GAMESTART,
    INGAME
}
public class GameManager : Singleton<GameManager>
{
    //���� ��� ����
    public GameMode gameMode;

    public GameObject gameStartUI;

    void Awake()
    {
        //ó�� ���� �� �κ�
        gameMode = GameMode.LOBBY;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeGameMode(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.LOBBY:
                break;
            case GameMode.GAMESTART:
                gameStartUI.SetActive(true);
                break;
            case GameMode.INGAME:
                break;
        }
    }
}
