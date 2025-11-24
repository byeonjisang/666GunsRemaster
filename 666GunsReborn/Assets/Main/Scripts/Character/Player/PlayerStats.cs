<<<<<<< HEAD
using UnityEngine;

namespace Character.Player
{
    public class PlayerStats
    {
        // 체력
        private float _baseHealth;
        private float _currentHealth;

        // 이동속도
        private float _baseMoveSpeed;
        private float _currentMoveSpeed;
        // 외부에서 접근 가능한 현재 이동속도
        public float CurrentMoveSpeed => _currentMoveSpeed;

        // 대쉬 개수
        private int _baseDashCount;
        private int _currentDashCount;
        // 외부에서 접근 가능한 현재 대쉬 개수
        public int CurrentDashCount => _currentDashCount;

        // 대쉬 거리
        private float _baseDashDistance;
        public float _currentDashDistance;
        // 외부에서 접근 가능한 현재 대쉬 거리
        public float CurrentDashDistance => _currentDashDistance;

        // 대쉬 쿨타임
        private float _baseDashCooldown;
        private float _currentDashCooldown;
        private float _dashCooldownTimer;

        /// <summary>
        /// 플레이어 스텟 초기화 메서드
        /// </summary>
        /// <param name="playerData"></param>
        public void Init(PlayerData playerData)
        {
            // 체력 초기화
            _baseHealth = playerData.health;
            _currentHealth = _baseHealth;

            // 이동속도 초기화
            _baseMoveSpeed = playerData.moveSpeed;
            _currentMoveSpeed = _baseMoveSpeed;

            // 대쉬 개수 초기화
            _baseDashCount = playerData.dashCount;
            _currentDashCount = _baseDashCount;

            // 대쉬 거리 초기화
            _baseDashDistance = playerData.dashDistance;
            _currentDashDistance = _baseDashDistance;

            // 대쉬 쿨타임 초기화
            _baseDashCooldown = playerData.dashCooldown;
            _currentDashCooldown = _baseDashCooldown;
            _dashCooldownTimer = 0f;
        }

        /// <summary>
        /// 데미지 처리 메서드
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool TakeDamage(float damage)
        {
            // 데미지 부여
            _currentHealth -= damage;

            return _currentHealth <= 0;
        }

        public void UseDash()
        {
            _currentDashCount -= 1;
        }

        /// <summary>
        /// 대쉬 쿨타임 업데이트 메서드
        /// </summary>
        public void DashCooldownUpdate()
        {
            if(_currentDashCount < _baseDashCount)
            {
                _dashCooldownTimer += Time.deltaTime;
                if(_dashCooldownTimer >= _currentDashCooldown)
                {
                    _currentDashCount++;
                    _dashCooldownTimer = 0f;
                }
            }
        }
    }
=======
using UnityEngine;

namespace Character.Player
{
    public class PlayerStats
    {
        // 체력
        private float _baseHealth;
        private float _currentHealth;

        // 이동속도
        private float _baseMoveSpeed;
        private float _currentMoveSpeed;
        // 외부에서 접근 가능한 현재 이동속도
        public float CurrentMoveSpeed => _currentMoveSpeed;

        // 대쉬 개수
        private int _baseDashCount;
        private int _currentDashCount;
        // 외부에서 접근 가능한 현재 대쉬 개수
        public int CurrentDashCount => _currentDashCount;

        // 대쉬 거리
        private float _baseDashDistance;
        public float _currentDashDistance;
        // 외부에서 접근 가능한 현재 대쉬 거리
        public float CurrentDashDistance => _currentDashDistance;

        // 대쉬 쿨타임
        private float _baseDashCooldown;
        private float _currentDashCooldown;
        private float _dashCooldownTimer;

        /// <summary>
        /// 플레이어 스텟 초기화 메서드
        /// </summary>
        /// <param name="playerData"></param>
        public void Init(PlayerData playerData)
        {
            // 체력 초기화
            _baseHealth = playerData.health;
            _currentHealth = _baseHealth;

            // 이동속도 초기화
            _baseMoveSpeed = playerData.moveSpeed;
            _currentMoveSpeed = _baseMoveSpeed;

            // 대쉬 개수 초기화
            _baseDashCount = playerData.dashCount;
            _currentDashCount = _baseDashCount;

            // 대쉬 거리 초기화
            _baseDashDistance = playerData.dashDistance;
            _currentDashDistance = _baseDashDistance;

            // 대쉬 쿨타임 초기화
            _baseDashCooldown = playerData.dashCooldown;
            _currentDashCooldown = _baseDashCooldown;
            _dashCooldownTimer = 0f;
        }

        /// <summary>
        /// 데미지 처리 메서드
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool TakeDamage(float damage)
        {
            // 데미지 부여
            _currentHealth -= damage;

            return _currentHealth <= 0;
        }

        public void UseDash()
        {
            _currentDashCount -= 1;
        }

        /// <summary>
        /// 대쉬 쿨타임 업데이트 메서드
        /// </summary>
        public void DashCooldownUpdate()
        {
            if(_currentDashCount < _baseDashCount)
            {
                _dashCooldownTimer += Time.deltaTime;
                if(_dashCooldownTimer >= _currentDashCooldown)
                {
                    _currentDashCount++;
                    _dashCooldownTimer = 0f;
                }
            }
        }
    }
>>>>>>> origin/main
}