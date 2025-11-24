using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerType : MonoBehaviour
{
    [SerializeField]
    private List<Character.Player.Player> players;

    private bool showDropdown = false; // 드롭다운 표시 여부
    private int selectedIndex = 0; // 선택된 항목의 인덱스
    private string[] options = { "Player1", "Player2"}; // 드롭다운 항목
    private GUIStyle dropdownStyle; // 드롭다운 스타일
    private GUIStyle buttonStyle; // 버튼 스타일

    private void OnGUI()
    {
        dropdownStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 16, // 글자 크기 설정
            alignment = TextAnchor.MiddleCenter // 텍스트 정렬
        };

        // 버튼 스타일 설정
        buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 20, // 버튼 글자 크기
            alignment = TextAnchor.MiddleCenter
        };

        // 드롭다운 버튼
        if (GUI.Button(new Rect(10, 10, 200, 40), options[selectedIndex], buttonStyle))
        {
            showDropdown = !showDropdown;
        }

        // 드롭다운 메뉴 표시
        if (showDropdown)
        {
            GUI.Box(new Rect(10, 50, 200, options.Length * 40), ""); // 드롭다운 배경 박스

            for (int i = 0; i < options.Length; i++)
            {
                // 각 옵션의 버튼
                if (GUI.Button(new Rect(10, 50 + (i * 40), 200, 40), options[i], dropdownStyle))
                {
                    selectedIndex = i; // 선택된 항목 업데이트
                    showDropdown = false; // 드롭다운 닫기
                    ExecuteAction(selectedIndex); // 선택된 항목에 따른 함수 실행
                }
            }
        }

        // 선택된 항목 표시
        GUI.Label(new Rect(10, 250, 200, 40), "Selected: " + options[selectedIndex], new GUIStyle(GUI.skin.label)
        {
            fontSize = 18, // 선택된 항목 글자 크기
            alignment = TextAnchor.MiddleLeft
        });
    }

    private void ExecuteAction(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Player1 Selected");
                //PlayerController.Instance.SelectPlayerType(players[0]);
                break;
            case 1:
                Debug.Log("Player2 Selected");
                //PlayerController.Instance.SelectPlayerType(players[1]);
                break;
            default:
                break;
        }
    }
}