using UnityEngine;
using UnityEngine.UI;

public class PlayerTypeSelect : MonoBehaviour
{
    private Dropdown playerTypeDropdown;

    private void Start()
    {
        playerTypeDropdown = GetComponentInChildren<Dropdown>();

        playerTypeDropdown.onValueChanged.AddListener(OnPlayerTypeChanged);
        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        playerTypeDropdown.value = (int)PlayerManager.Instance.PlayerType;
    }

    private void OnPlayerTypeChanged(int index)
    {
        PlayerType selectedType = (PlayerType)index;
        PlayerManager.Instance.SetPlayerType(selectedType);
        Debug.Log("Selected Player Type: " + selectedType);
    }
}