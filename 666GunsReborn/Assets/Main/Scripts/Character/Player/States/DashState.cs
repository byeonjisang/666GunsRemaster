using UnityEngine;

namespace Character.Player
{
    // 대쉬 상태
    public class DashState : IState
    {
        private Player _player;

        private float dashTime;
        private float dashTimer = 0f;

        public DashState(Player player)
        {
            _player = player;
        }

        public void EnterState()
        {
            // 대쉬 카운팅 및 애니메이션 실행
            _player.Stat.UseDash();
            _player.Anim.SetBool("IsDash", true);
            // 대쉬 시간 계산
            dashTime = _player.Stat.CurrentDashDistance / 50;
            dashTimer = 0f;

            // 대쉬 실행
            _player.DashSystem.Dash(_player.InputDirection, dashTime);
        }

        public void UpdateState()
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashTime)
            {
                _player.StateContext.TransitionTo(_player.StateContext.IdleState);
            }
        }

        public void ExitState()
        {
            _player.Anim.SetBool("IsDash", false);
        }
    }
}

// 세이브
// public DashState(Player player) : base(player) { }

// public override void HandleInput(Vector3 direction)
// {
//     // 대쉬 개수 감소
//     player.Stat.CurrentDashCount--;

//     // 이동을 하고 있지 않다면 바라보는 방향으로 대쉬
//     if (direction.sqrMagnitude < 0.1f)
//         direction = player.transform.forward.normalized;
//     // 대쉬 이동
//     player.rigid.velocity = direction * player.Stat.CurrentDashDistance;
// }

// // private IEnumerator DashCoroutine()
// // {
// //     float dashTime = Stat.CurrentDashDistance / 50f;
// //     _currentState?.HandleInput(gameObject.transform.forward);
// //     yield return new WaitForSeconds(dashTime);

// //     player.SetState(PlayerStateType.Idle);
// // }

// public override void EnterState()
// {
//     player.anim.SetBool("IsDash", true);
// }

// public override void ExitState()
// {
//     player.anim.SetBool("IsDash", false);
// }