using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponChangeReload : MonoBehaviour
{
    [Header("Weapon Change Cooldown UI Color")]
    [SerializeField]
    private Color weaponChangeCooldownImageColor;

    private Image weaponChangeCooldownImage;
    private Text weaponChangeCooldownText;

    private bool isVisible = false;

    private void Awake()
    {
        weaponChangeCooldownImage = GetComponent<Image>();
        weaponChangeCooldownText = GetComponentInChildren<Text>();

        if (weaponChangeCooldownImage == null || weaponChangeCooldownText == null)
        {
            Debug.LogError("Weapon Change Cooldown UI components are not set up correctly.");
        }

        weaponChangeCooldownImage.color = Color.white;
        weaponChangeCooldownText.gameObject.SetActive(false);
    }

    private void Start()
    {
        WeaponUIEvents.OnUpdateCooldownUI += UpdateCooldownUI;
    }

    public void UpdateCooldownUI(float currentTime)
    {
        if (currentTime > 0f)
        {
            if (!isVisible)
            {
                Debug.Log("Weapon Change Cooldown UI is now visible.");
                isVisible = true;
                weaponChangeCooldownImage.color = weaponChangeCooldownImageColor;
                weaponChangeCooldownText.gameObject.SetActive(true);
                weaponChangeCooldownImage.transform.SetAsLastSibling();
                weaponChangeCooldownText.transform.SetAsLastSibling();
            }

            weaponChangeCooldownText.text = Mathf.Ceil(currentTime).ToString();
        }
        else
        {
            if (isVisible)
            {
                isVisible = false;
                weaponChangeCooldownImage.color = Color.white;
                weaponChangeCooldownText.gameObject.SetActive(false);
            }
        }
    }
}
