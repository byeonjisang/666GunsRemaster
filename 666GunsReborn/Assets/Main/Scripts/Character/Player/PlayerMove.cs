using UnityEngine;

namespace Character.Player
{
    public class PlayerMove
    {
        // 플레이어 참조
        private Player _player;

        // 생성자
        public PlayerMove(Player player)
        {
            _player = player;
        }

        /// <summary>
        /// 플레이어 이동 처리 메서드
        /// </summary>
        public void Move()
        {   
            // 이동 방향에 따른 플레이어 이동
            Vector3 direction = _player.InputDirection.normalized;
            Vector3 moveVelocity = new Vector3(direction.x, 0f, direction.z) * _player.Stat.CurrentMoveSpeed;
            _player.Rigid.velocity = new Vector3(moveVelocity.x, _player.Rigid.velocity.y, moveVelocity.z);

            // 시선처리
            LookAt(direction);
        }
        
        // 시선 처리 메서드
        private void LookAt(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                Debug.Log($"[PlayerMove] NearsetEnemy: {_player.Scanner.NearestEnemy}, IsAttacking: {_player.AttackSystem.IsAttacking}");
                if (_player.Scanner.NearestEnemy != null && _player.AttackSystem.IsAttacking)
                {
                    Debug.Log("플레이어가 적을 스캔함");

                    //적의 위치로 회전
                    direction = _player.Scanner.NearestEnemy.transform.position - _player.transform.position;
                    direction.y = 0f;
                }

                Quaternion targetAngle = Quaternion.LookRotation(direction);
                _player.Rigid.rotation = targetAngle;
            }
        }
    }
}