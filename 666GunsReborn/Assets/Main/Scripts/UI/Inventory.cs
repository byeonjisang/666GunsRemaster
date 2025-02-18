using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryUI;

    //플레이어에 존재하는 재화 및 데이터와 연결
    Player currentPlayer;

    public Transform inventoryContainer;
    public GameObject inventoryItemPrefab;
    public Text statusText;

    private List<string> inventoryItems = new List<string>();

    private void Start()
    {
        LoadInventory();
    }

    public void AddItem(string itemName)
    {
        inventoryItems.Add(itemName);
        UpdateInventoryUI();
        SaveInventory();
    }

    public void RemoveItem(string itemName)
    {
        if (inventoryItems.Contains(itemName))
        {
            inventoryItems.Remove(itemName);
            UpdateInventoryUI();
            SaveInventory();
        }
    }

    private void UpdateInventoryUI()
    {
        //인벤토리에 존재하는 아이템이 추가 및 삭제 시 매번 업데이트 호출 필요
        foreach (Transform child in inventoryContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string item in inventoryItems)
        {
            GameObject itemObject = Instantiate(inventoryItemPrefab, inventoryContainer);
            itemObject.GetComponentInChildren<Text>().text = item;
        }

        statusText.text = "Inventory Updated";
    }

    private void SaveInventory()
    {
        PlayerPrefs.SetString("Inventory", string.Join(",", inventoryItems));
    }

    private void LoadInventory()
    {
        string savedData = PlayerPrefs.GetString("Inventory", "");
        if (!string.IsNullOrEmpty(savedData))
        {
            inventoryItems = new List<string>(savedData.Split(','));
        }

        UpdateInventoryUI();
    }

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
