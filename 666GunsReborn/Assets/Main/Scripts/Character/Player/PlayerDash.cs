using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDash
    {
        // 플레이어 참조
        private Player _player;

        // 생성자
        public PlayerDash(Player player)
        {
            _player = player;
        }  

        /// <summary>
        /// 대쉬 실행 메서드
        /// </summary>
        public void Dash(Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.1f)
                direction = _player.transform.forward.normalized;
            
            // 대쉬 이동
            _player.Rigid.velocity = direction * _player.Stat.CurrentDashDistance;
        }
    }
}