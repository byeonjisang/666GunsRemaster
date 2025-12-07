using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Datas/PlayerData", order = 0)]
    public class PlayerData : CharacterData
    {
        // 대쉬 개수
        [SerializeField] private int dashCount;
        public int DashCount => dashCount;
        // 대쉬 거리
        [SerializeField] private float dashDistance;
        public float DashDistance => dashDistance;
        // 대쉬 쿨타임
        [SerializeField] private float dashCooldown;
        public float DashCooldown => dashCooldown;
    }
}
