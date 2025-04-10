using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject _gameStartUI;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _gameStartUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _gameStartUI.SetActive(false);
        }
    }

    public void Accept()
    {
        //수락 시 씬 전환되면서 게임모드 변경
        //GameManager.Instance.ChangeGameMode(gameMode);
        Debug.Log("게임 모드 변경" + GameManager.Instance.gameMode);
    }

    public void Reject()
    {

    }
}