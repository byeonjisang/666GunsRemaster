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

    //���� ��ü ���� UI
    [Header("����")]
    [SerializeField]
    private Image WeaponChangeImage;
    [SerializeField]
    private Text WeaponChangeText;
    [SerializeField]
    private Image WeaponGetButton;
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
    [SerializeField]
    private Image OverhitImage;
    private Coroutine overhitCoroutine;
    //���� ���� UI
    [Header("����")]
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

    //�Ѿ� ���� UI ������Ʈ
    public void UpdateBulletCount(int currentBulletCount, int magazineCount)
    {
        if (currentBulletCount == -1)
        {
            WeaponChangeText.text = "��";
        }
        else
        {
            WeaponChangeText.text = magazineCount + " / " + currentBulletCount;
        }
    }
    //���� ��ü ��� ���� UI
    public void UpdateWeaponImage(Sprite weaponImage)
    {
        WeaponChangeImage.sprite = weaponImage;
    }
    //���� �ݱ� ��� ���� UI
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
    //������Ʈ �̹��� ���
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

    //���� ���
    public void ShowBuff(int index, Sprite buffImage, string buffName, string BuffContent)
    {
        BuffIcons[index].sprite = buffImage;
        BuffNames[index].text = buffName;
        BuffContents[index].text = BuffContent;
    }
    
    //���� â ��/����
    public void BuffWindowOnOff(bool isOn)
    {
        BuffObject.gameObject.SetActive(isOn);
    }
    //���� ��ư ����
    public void OnButtonBuff(int index, Action<string> applyBuff, string buffType)
    {
        BuffButtons[index].onClick.AddListener(() => applyBuff(buffType));
    }
}