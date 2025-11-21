using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Datas/PlayerData", order = 0)]
    public class PlayerData : CharacterData
    {
        public int dashCount;
        public float dashDistance;
        public float dashCooldown;
    }
}
