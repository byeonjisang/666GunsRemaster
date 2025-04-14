using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    //���� ��� ����
    public GameMode _gameMode;

    //�÷��̾� ���� ��ȭ ǥ��
    public Text _userCoinText;

    public Button _settingButton;

    void Awake()
    {
        //ó�� ���� �� �κ�
        _gameMode = GameMode.LOBBY;

        //GameManager�� �ı��Ǹ� �ȵ�. �׻� �� ���� �� ����
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
