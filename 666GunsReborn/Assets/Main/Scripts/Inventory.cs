using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryUI;

    Player currentPlayer;

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
}
