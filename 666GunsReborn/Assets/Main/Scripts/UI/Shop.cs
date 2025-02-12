using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryUI;

    Player currentPlayer;

    //������ ���� ������
    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;

    public Text noCoinText;
    public string[] textData;

    void Enter(Player player)
    {
        currentPlayer = player;

        //�κ��丮 â Ȱ��ȭ
        inventoryUI.anchoredPosition = Vector3.zero;

    }

    void Exit()
    {
        //�κ��丮 â ����
        inventoryUI.anchoredPosition = Vector3.down * 1000;
    }

    void Buy(int index)
    {
        int price = itemPrice[index];
    }

    //���â
    IEnumerator Talk()
    {
        noCoinText.text = textData[1];
        yield return new WaitForSeconds(1f);
        noCoinText.text = textData[0];
    }
}
