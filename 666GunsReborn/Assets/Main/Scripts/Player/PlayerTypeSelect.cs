using UnityEngine;
using UnityEngine.UI;

public class PlayerTypeSelect : MonoBehaviour
{
    private Dropdown playerTypeDropdown;

    private void Start()
    {
        playerTypeDropdown = GetComponentInChildren<Dropdown>();

        playerTypeDropdown.onValueChanged.AddListener(OnPlayerTypeChanged);
        //playerTypeDropdown.
    }

    private void OnPlayerTypeChanged(int index)
    {
        PlayerType selectedType = (PlayerType)index;
        Debug.Log("Selected Player Type: " + selectedType);
    }
}