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
    [SerializeField]
    private Image weaponChangeImage;
    [SerializeField]
    private Text weaponChangeText;
    [SerializeField]
    private Image weaponGetButton;
    [SerializeField]
    private Slider PlayerHealthSlider;
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
    public void UpdatePlayerHealth(int maxHealth, int health)
    {
        PlayerHealthSlider.value = health / maxHealth;
    }
    public void UpdatePlayerDash(int dashNumber)
    {

    }
}
