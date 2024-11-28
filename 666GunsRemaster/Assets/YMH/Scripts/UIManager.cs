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

    //���� ��ü ���� UI
    [Header("����")]
    [SerializeField]
    private Image weaponChangeImage;
    [SerializeField]
    private Text weaponChangeText;
    [SerializeField]
    private Image weaponGetButton;
    //ü�� ���� UI
    [Header("ü��")]
    [SerializeField]
    private Slider PlayerHealthSlider;
    //�뽬 ���� UI
    [Header("�뽬")]
    [SerializeField]
    private List<Image> DashBackgroundImages;
    [SerializeField]
    private List<Image> DashImages;
    //������Ʈ ���� UI
    [Header("������Ʈ")]
    [SerializeField]
    private List<Slider> OverhitSliders;
    //���� ���� UI
    [Header("����")]
    [SerializeField]    
    private List<Image> BuffIcons;
    [SerializeField]
    private List<Text> BuffNames;
    [SerializeField]
    private List<Text> BuffContents;

    //�Ѿ� ���� UI ������Ʈ
    public void UpdateBulletCount(int currentBulletCount, int magazineCount)
    {
        if (currentBulletCount == -1)
        {
            weaponChangeText.text = "��";
        }
        else
        {
            weaponChangeText.text = magazineCount + " / " + currentBulletCount;
        }
    }
    //���� ��ü ��� ���� UI
    public void UpdateWeaponImage(Sprite weaponImage)
    {
        weaponChangeImage.sprite = weaponImage;
    }
    //���� �ݱ� ��� ���� UI
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
    //ü�� ������ UI
    public void UpdatePlayerHealth(float maxHealth, float health)
    {
        PlayerHealthSlider.value = health / maxHealth;
    }
    //�÷��̾� �뽬 �ʱ� UI ����
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
    //�÷��̾� �뽬 UI ������Ʈ
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

    //������Ʈ ������ UI ������Ʈ
    public void UpdateOverhitSlider(int weaponIndex, float maxOverhit, float currentOverhit)
    {
        OverhitSliders[weaponIndex].value = maxOverhit / currentOverhit;
    }

    //���� ���
    public void ShowBuff(int index, Sprite buffImage, string buffName, string BuffContent)
    {
        BuffIcons[index].sprite = buffImage;
        BuffNames[index].text = buffName;
        BuffContents[index].text = BuffContent;
    }
}