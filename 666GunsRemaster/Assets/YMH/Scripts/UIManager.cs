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
    }
    //체력 게이지 UI
    public void UpdatePlayerHealth(int maxHealth, int health)
    {
        PlayerHealthSlider.value = health / maxHealth;
    }
    public void UpdatePlayerDash(int dashNumber)
    {

    }
}
