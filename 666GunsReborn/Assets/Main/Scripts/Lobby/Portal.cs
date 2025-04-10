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
        //���� �� �� ��ȯ�Ǹ鼭 ���Ӹ�� ����
        //GameManager.Instance.ChangeGameMode(gameMode);
        Debug.Log("���� ��� ����" + GameManager.Instance.gameMode);
    }

    public void Reject()
    {

    }
}