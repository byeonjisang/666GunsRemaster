using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Stage Select Data")]
    // 스테이지 선택 창에서 선택한 내용들 저장한 데이터
    [SerializeField] private StageSelectData _stageSelectData;

    [Header("Player")]
    [SerializeField] private Character.Player.Player _player;

    private void Start()
    {
        StartStage();
    }

    // 스테이지 시작
    private void StartStage()
    {
        _player.Init(_stageSelectData.SelectedWeaponsID);
    }
}
