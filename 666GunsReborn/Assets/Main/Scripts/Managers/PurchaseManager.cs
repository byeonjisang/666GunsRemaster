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

    private List<bool> purchasedCostumes = new List<bool>(); // 구매 여부 저장

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

        //GameManager.Instance.SetCoinTextTarget(lobbyCoinText);

        //// 구매 여부 초기화 (모두 미구매로)
        //for (int i = 0; i < costumeButtons.Count; i++)
        //{
        //    purchasedCostumes.Add(false);
        //}

        //// 버튼 클릭 이벤트 등록
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
            // 구매 여부 초기화 (모두 미구매로)
            for (int i = 0; i < costumeButtons.Count; i++)
            {
                purchasedCostumes.Add(false);
            }

            // 버튼 클릭 이벤트 등록
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
                lockCostumeImages[i].SetActive(false); // 구매했으므로 잠금 해제
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
            Debug.Log($"코스튬 {index} 이미 구매함");
            return;
        }

        int price = costumePrices[index];
        int userCoin = GameManager.Instance._coin;

        if (userCoin >= price)
        {
            GameManager.Instance._coin -= price;
            GameManager.Instance.ShowCoinText();

            purchasedCostumes[index] = true;

            Debug.Log($"코스튬 {index} 구매 완료! 남은 코인: {GameManager.Instance._coin}");

            // 다시 전체 버튼 상태 갱신
            UpdateCostumeButtons();
        }
        else
        {
            Debug.Log("코인이 부족하여 구매할 수 없습니다.");
        }
    }
}
