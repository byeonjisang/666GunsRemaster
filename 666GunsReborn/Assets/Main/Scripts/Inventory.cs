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

        //인벤토리 창 활성화
        inventoryUI.anchoredPosition = Vector3.zero;

    }

    void Exit()
    {
        //인벤토리 창 내림
        inventoryUI.anchoredPosition = Vector3.down * 1000;
    }
}
