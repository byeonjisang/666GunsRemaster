using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryUI;

    Player currentPlayer;

    //아이템 관련 변수들
    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;

    public Text noCoinText;
    public string[] textData;

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

    void Buy(int index)
    {
        int price = itemPrice[index];
    }

    //경고창
    IEnumerator Talk()
    {
        noCoinText.text = textData[1];
        yield return new WaitForSeconds(1f);
        noCoinText.text = textData[0];
    }
}
