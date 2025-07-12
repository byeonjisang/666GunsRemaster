using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : Singleton<PurchaseManager>
{

    [SerializeField] private Canvas purchaseCanvas;
    [SerializeField] private List<Button> costumeButtons;
    [SerializeField] private List<GameObject> lockCostumeImages;
    [SerializeField] private List<int> costumePrices;
    [SerializeField] private GameObject lobbyCoinText;

    private List<bool> purchasedCostumes = new List<bool>(); // ���� ���� ����

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

        //GameManager.Instance.SetCoinTextTarget(lobbyCoinText);

        //// ���� ���� �ʱ�ȭ (��� �̱��ŷ�)
        //for (int i = 0; i < costumeButtons.Count; i++)
        //{
        //    purchasedCostumes.Add(false);
        //}

        //// ��ư Ŭ�� �̺�Ʈ ���
        //for (int i = 0; i < costumeButtons.Count; i++)
        //{
        //    int index = i;
        //    costumeButtons[i].onClick.AddListener(() => TryPurchaseCostume(index));
        //}

        //UpdateCostumeButtons();
    }

    private void Update()
    {
        if(purchaseCanvas.gameObject.activeSelf)
        {
            // ���� ���� �ʱ�ȭ (��� �̱��ŷ�)
            for (int i = 0; i < costumeButtons.Count; i++)
            {
                purchasedCostumes.Add(false);
            }

            // ��ư Ŭ�� �̺�Ʈ ���
            for (int i = 0; i < costumeButtons.Count; i++)
            {
                int index = i;
                costumeButtons[i].onClick.AddListener(() => TryPurchaseCostume(index));
            }

            UpdateCostumeButtons();
        }
    }

    public void UpdateCostumeButtons()
    {
        int userCoin = GameManager.Instance._coin;

        for (int i = 0; i < costumeButtons.Count; i++)
        {
            if (purchasedCostumes[i])
            {
                costumeButtons[i].interactable = false;
                lockCostumeImages[i].SetActive(false); // ���������Ƿ� ��� ����
            }
            else
            {
                bool canBuy = userCoin >= costumePrices[i];
                costumeButtons[i].interactable = canBuy;
                lockCostumeImages[i].SetActive(!canBuy);
            }
        }
    }

    public void TryPurchaseCostume(int index)
    {
        if (purchasedCostumes[index])
        {
            Debug.Log($"�ڽ�Ƭ {index} �̹� ������");
            return;
        }

        int price = costumePrices[index];
        int userCoin = GameManager.Instance._coin;

        if (userCoin >= price)
        {
            GameManager.Instance._coin -= price;
            GameManager.Instance.ShowCoinText();

            purchasedCostumes[index] = true;

            Debug.Log($"�ڽ�Ƭ {index} ���� �Ϸ�! ���� ����: {GameManager.Instance._coin}");

            // �ٽ� ��ü ��ư ���� ����
            UpdateCostumeButtons();
        }
        else
        {
            Debug.Log("������ �����Ͽ� ������ �� �����ϴ�.");
        }
    }
}
