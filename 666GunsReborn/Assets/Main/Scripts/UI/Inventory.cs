using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryUI;

    //�÷��̾ �����ϴ� ��ȭ �� �����Ϳ� ����
    Character.Player.Player currentPlayer;

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
        //�κ��丮�� �����ϴ� �������� �߰� �� ���� �� �Ź� ������Ʈ ȣ�� �ʿ�
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

    void Enter(Character.Player.Player player)
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
