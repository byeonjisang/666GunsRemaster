using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public string playerType;
        public int health;
        public float moveSpeed;
        public float dashSpeed;
        public float dashDuration;
        public float dashCooldown;
    }
