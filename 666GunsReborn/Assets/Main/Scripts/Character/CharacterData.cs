using UnityEngine;

namespace Character
{
    public class CharacterData : ScriptableObject
    {
        // 캐릭터 이름
        [SerializeField]
        private string characterName;
        public string CharacterName => characterName;
        
        // 캐릭터 체력
        [SerializeField]
        private int health;
        public int Health => health;
        
        // 캐릭터 이동 속도
        [SerializeField]
        private float moveSpeed;
        public float MoveSpeed => moveSpeed;
    }
}