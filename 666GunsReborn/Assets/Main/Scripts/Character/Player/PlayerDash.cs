<<<<<<< HEAD
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
        public void Dash(Vector3 direction, float dashTime)
        {
            _player.StartCoroutine(DashCoroutine(direction, dashTime));
        }

        // 대쉬 코루틴
        private IEnumerator DashCoroutine(Vector3 direction, float dashTime)
        {
            if (direction.sqrMagnitude < 0.1f)
                direction = _player.transform.forward.normalized;
            
            // 대쉬 이동
            _player.Rigid.velocity = direction * _player.Stat.CurrentDashDistance;
            yield return new WaitForSeconds(dashTime);
            
            _player.Rigid.velocity = Vector3.zero;
        }
    }
=======
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
        public void Dash(Vector3 direction, float dashTime)
        {
            _player.StartCoroutine(DashCoroutine(direction, dashTime));
        }

        // 대쉬 코루틴
        private IEnumerator DashCoroutine(Vector3 direction, float dashTime)
        {
            if (direction.sqrMagnitude < 0.1f)
                direction = _player.transform.forward.normalized;
            
            // 대쉬 이동
            _player.Rigid.velocity = direction * _player.Stat.CurrentDashDistance;
            yield return new WaitForSeconds(dashTime);
            
            _player.Rigid.velocity = Vector3.zero;
        }
    }
>>>>>>> origin/main
}