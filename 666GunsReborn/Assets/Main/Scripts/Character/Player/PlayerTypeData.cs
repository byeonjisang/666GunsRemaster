using System;
using UnityEngine;

namespace Character.Player
{
    [CreateAssetMenu(fileName = "PlayerTypeData", menuName = "ScriptableObjects/PlayerType", order = 1)]
    public class PlayerTypeData : ScriptableObject
    {
        [Header("플레이어 타임 설정")]
        public PlayerType playerType;
        public int level;

        [Serializable]
        // 플레이어 데이터
        public struct LevelRow
        {
            public PlayerLevelData[] levelData;
        }
        [Header("플레이어 타입별 데이터")]
        [SerializeField] private LevelRow[] playerLevelData = new LevelRow[3];
        public LevelRow[] PlayerLevelData => playerLevelData;
    }   
}