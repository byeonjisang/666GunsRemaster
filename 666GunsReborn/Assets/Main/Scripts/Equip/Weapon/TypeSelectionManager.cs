using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypeSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject typeSelectionPanel;

    [SerializeField] private Dropdown playerTypeDropdown;

    [SerializeField] private Dropdown weapon1TypeDropdown;
    [SerializeField] private Dropdown weapon2TypeDropdown;

    private void Start()
    {
        // Initialize the dropdowns with the current player and weapon types
        playerTypeDropdown.value = (int)PlayerType.Attack;
        weapon1TypeDropdown.value = (int)WeaponType.Pistol;
        weapon2TypeDropdown.value = (int)WeaponType.Pistol;

        // Add listeners to handle changes
        playerTypeDropdown.onValueChanged.AddListener(OnPlayerTypeChanged);
        weapon1TypeDropdown.onValueChanged.AddListener(OnWeapon1TypeChanged);
        weapon2TypeDropdown.onValueChanged.AddListener(OnWeapon2TypeChanged);

        Time.timeScale = 0f; // Pause the game when the type selection panel is active
    }

    private void OnPlayerTypeChanged(int index)
    {

    }

    private void OnWeapon1TypeChanged(int index)
    {
        WeaponType selectedType = (WeaponType)index;
        PlayerManager.Instance.SetWeaponType(selectedType, 0);
        Debug.Log("Selected Weapon 1 Type: " + selectedType);
    }

    private void OnWeapon2TypeChanged(int index)
    {
        WeaponType selectedType = (WeaponType)index;
        PlayerManager.Instance.SetWeaponType(selectedType, 1);
        Debug.Log("Selected Weapon 2 Type: " + selectedType);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerManager.Instance.InitializePlayer(playerObject);

        WeaponManager.Instance.Initialized(weapon1TypeDropdown.value, weapon2TypeDropdown.value);

        typeSelectionPanel.SetActive(false);
    }
}
