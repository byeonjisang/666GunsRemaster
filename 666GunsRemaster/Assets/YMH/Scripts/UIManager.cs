using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Image weaponChangeImage;
    [SerializeField]
    private Text weaponChangeText;
    [SerializeField]
    private Image weaponGetButton;
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
    //버프 관련 UI
    [Header("버프")]
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
            weaponChangeText.text = "∞";
        }
        else
        {
            weaponChangeText.text = magazineCount + " / " + currentBulletCount;
        }
    }
    //무기 교체 기능 관련 UI
    public void UpdateWeaponImage(Sprite weaponImage)
    {
        weaponChangeImage.sprite = weaponImage;
    }
    //무기 줍기 기능 관련 UI
    public void UpdateGetWeaponImage(Sprite weaponImage)
    {
        if (weaponImage == null)
        {
            weaponGetButton.sprite = null;
        }
        else
        {
            weaponGetButton.sprite = weaponImage;
        }
        weaponGetButton.SetNativeSize();
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

    //버프 출력
    public void ShowBuff(int index, Sprite buffImage, string buffName, string BuffContent)
    {
        BuffIcons[index].sprite = buffImage;
        BuffNames[index].text = buffName;
        BuffContents[index].text = BuffContent;
    }
}