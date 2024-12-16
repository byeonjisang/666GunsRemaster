using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //무기 교체 관련 UI
    [Header("무기")]
    [SerializeField]
    private Image WeaponChangeImage;
    [SerializeField]
    private Text WeaponChangeText;
    [SerializeField]
    private Image WeaponGetButton;
    //체력 관련 UI
    [Header("체력")]
    [SerializeField]
    private Slider PlayerHealthSlider;
    //대쉬 관련 UI
    [Header("대쉬")]
    [SerializeField]
    private List<Image> DashBackgroundImages;
    [SerializeField]
    private List<Image> DashImages;
    //오버히트 관련 UI
    [Header("오버히트")]
    [SerializeField]
    private List<Slider> OverhitSliders;
    [SerializeField]
    private Image OverhitImage;
    private Coroutine overhitCoroutine;
    //버프 관련 UI
    [Header("버프")]
    [SerializeField]
    private GameObject BuffObject;
    [SerializeField]
    private List<Button> BuffButtons;
    [SerializeField]    
    private List<Image> BuffIcons;
    [SerializeField]
    private List<Text> BuffNames;
    [SerializeField]
    private List<Text> BuffContents;

    //총알 개수 UI 업데이트
    public void UpdateBulletCount(int currentBulletCount, int magazineCount)
    {
        if (currentBulletCount == -1)
        {
            WeaponChangeText.text = "∞";
        }
        else
        {
            WeaponChangeText.text = magazineCount + " / " + currentBulletCount;
        }
    }
    //무기 교체 기능 관련 UI
    public void UpdateWeaponImage(Sprite weaponImage)
    {
        WeaponChangeImage.sprite = weaponImage;
    }
    //무기 줍기 기능 관련 UI
    public void UpdateGetWeaponImage(Sprite weaponImage)
    {
        if (weaponImage == null)
        {
            WeaponGetButton.sprite = null;
        }
        else
        {
            WeaponGetButton.sprite = weaponImage;
        }
        WeaponGetButton.SetNativeSize();
    }
    //체력 게이지 UI
    public void UpdatePlayerHealth(float maxHealth, float health)
    {
        PlayerHealthSlider.value = health / maxHealth;
    }
    //플레이어 대쉬 초기 UI 설정
    public void PlayerDashUiInit(int dashCount)
    {
        for(int i = 0; i < DashBackgroundImages.Count; i++)
        {
            DashBackgroundImages[i].gameObject.SetActive(false);
            DashImages[i].gameObject.SetActive(false);
        }

        for(int i  = 0; i < dashCount; i++)
        {
            DashBackgroundImages[i].gameObject.SetActive(true);
            DashImages[i].gameObject.SetActive(true);
        }
    }
    //플레이어 대쉬 UI 업데이트
    public void UpdatePlayerDash(int dashCount, int dashNumber)
    {
        for (int i = 0; i < dashCount; i++)
        {
            DashImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < dashNumber; i++)
        {
            DashImages[i].gameObject.SetActive(true);
        }
    }

    //오버히트 게이지 UI 업데이트
    public void UpdateOverhitSlider(int weaponIndex, float maxOverhit, float currentOverhit)
    {
        OverhitSliders[weaponIndex].value = maxOverhit / currentOverhit;
    }
    //오비히트 이미지 출력
    public void OnOverhitImage()
    {
        OverhitImage.gameObject.SetActive(true);
        OverhitImage.color = new Color(1, 1, 1, 0.8f);

        overhitCoroutine = StartCoroutine(BlinkOverhit());
    }
    public void OffOverhitImage()
    {
        OverhitImage.gameObject.SetActive(false);

        StopCoroutine(overhitCoroutine);
    }
    private IEnumerator BlinkOverhit()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            float alpha = OverhitImage.color.a;
            alpha -= 0.4f;
            if (alpha <= 0)
                alpha = 0.8f;

            OverhitImage.color = new Color(1, 1, 1, alpha);
        }
    }

    //버프 출력
    public void ShowBuff(int index, Sprite buffImage, string buffName, string BuffContent)
    {
        BuffIcons[index].sprite = buffImage;
        BuffNames[index].text = buffName;
        BuffContents[index].text = BuffContent;
    }
    
    //버프 창 온/오프
    public void BuffWindowOnOff(bool isOn)
    {
        BuffObject.gameObject.SetActive(isOn);
    }
    //버프 버튼 설정
    public void OnButtonBuff(int index, Action<string> applyBuff, string buffType)
    {
        BuffButtons[index].onClick.AddListener(() => applyBuff(buffType));
    }
}