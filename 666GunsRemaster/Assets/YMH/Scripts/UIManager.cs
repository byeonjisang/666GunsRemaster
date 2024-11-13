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
    [SerializeField]
    private Image weaponChangeImage;
    [SerializeField]
    private Text weaponChangeText;
    [SerializeField]
    private Image weaponGetButton;
    [SerializeField]
    private Slider PlayerHealthSlider;
    [SerializeField]
    private List<Image> DashBackgroundImages;
    [SerializeField]
    private List<Image> DashImages;

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
    }
    //ü�� ������ UI
    public void UpdatePlayerHealth(float maxHealth, float health)
    {
        PlayerHealthSlider.value = health / maxHealth;
    }
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
}
