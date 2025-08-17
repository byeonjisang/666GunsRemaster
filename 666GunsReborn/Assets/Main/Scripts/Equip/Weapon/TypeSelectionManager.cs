using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class TypeSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject typeSelectionPanel;

    [SerializeField] private Dropdown playerTypeDropdown;

    // 무기 드롭다운 관련 변수
    [SerializeField] private Dropdown[] weaponTypeDropdowns;
    private HashSet<int>[] disabledBySlot = new HashSet<int>[2];
    private int[] weaponTypeOldValues = new int[2];

    private void Start()
    {
        playerTypeDropdown.value = (int)PlayerType.Attack;
        playerTypeDropdown.onValueChanged.AddListener(OnPlayerTypeChanged);

        // 무기 선택지 초기화
        SetWeaponDropdown();

        Time.timeScale = 0f;
    }

    #region Select Weapon dropdown
    private void SetWeaponDropdown()
    {
        // 드롭다운 초기화
        weaponTypeDropdowns[0].options.Clear();
        weaponTypeDropdowns[1].options.Clear();

        // Enum에 정의된 무기들 드롭다운에 추가
        foreach (WeaponID weaponId in System.Enum.GetValues(typeof(WeaponID)))
        {
            weaponTypeDropdowns[0].options.Add(new Dropdown.OptionData(weaponId.ToString()));
            weaponTypeDropdowns[1].options.Add(new Dropdown.OptionData(weaponId.ToString()));
        }

        //임시
        weaponTypeDropdowns[0].value = (int)WeaponID.Pistol_Slide;
        weaponTypeDropdowns[1].value = (int)WeaponID.Revolver;

        // 여기부터는 드롭다운 선택 시 잠금되는 기능 구현(못해서 나중으로 미룸)
        // Default 무기들로 설정
        //OnWeaponTypeChange(0, (int)WeaponID.Pistol_Slide);
        //OnWeaponTypeChange(1, (int)WeaponID.Revolver);

        // 드롭다운 값 변경 리스너 등록
        //weaponTypeDropdowns[0].onValueChanged.AddListener(value => OnWeaponTypeChange(0, value));
        //weaponTypeDropdowns[1].onValueChanged.AddListener(value => OnWeaponTypeChange(1, value));
    }

    private void OnWeaponTypeChange(int value)
    {
        
    }

    private void OnWeaponTypeChange(int weaponSlot, int value)
    {
        weaponTypeOldValues[weaponSlot] = value;

        // 필요 변수 초기화
        int otherSlot = 1 - weaponSlot;
        int oldValue = weaponTypeOldValues[weaponSlot];

        // 클릭을 잠그고 풀 무기들
        if (value != oldValue)
        {
            disabledBySlot[otherSlot].Add(value);
            disabledBySlot[otherSlot].Remove(oldValue);
        }

        UpdateOpenListUI(weaponSlot);
        UpdateOpenListUI(otherSlot);
    }

    private void UpdateOpenListUI(int slot)
    {
        var list = FindOpenListRoot(weaponTypeDropdowns[slot]);
        if (list == null)
        {
            Debug.LogError("Dropdown List not found.");
            return;
        }

        var toggles = list.GetComponentsInChildren<Toggle>(true);
        for (int i = 0; i < toggles.Length; i++)
        {
            bool isLocked = disabledBySlot[slot].Contains(i);
            toggles[i].interactable = !isLocked;

            var txt = toggles[i].GetComponentInChildren<Text>();
            txt.color = isLocked ? Color.gray : Color.black;
        }
    }

    private Transform FindOpenListRoot(Dropdown dd)
    {
        var rts = dd.transform.GetComponentsInChildren<RectTransform>(true);
        foreach (var rt in rts)
            if (rt.name == "Dropdown List")
                return rt.transform;
        return null;
    }
    #endregion

    #region Select Player Type
    private void OnPlayerTypeChanged(int index)
    {

    }
    #endregion

    #region Start Game
    public void StartGame()
    {
        Time.timeScale = 1f;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerManager.Instance.SetPlayerType((PlayerType)playerTypeDropdown.value);
        PlayerManager.Instance.InitializePlayer(playerObject);

        //WeaponManager.Instance.Initialized(weapon1TypeDropdown.value, weapon2TypeDropdown.value);
        WeaponManager1.Instance.Initialization(weaponTypeDropdowns[0].value, weaponTypeDropdowns[1].value);

        typeSelectionPanel.SetActive(false);
    }
    #endregion
}