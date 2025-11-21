using UnityEngine;

namespace Character.Player
{
    // 이동 상태
    public class MoveState : IState
    {
        private Player _player;

        public MoveState(Player player)
        {
            _player = player;
        }

        public void EnterState()
        {
            _player.Anim.SetFloat("Speed", _player.InputDirection.magnitude);
        }

        public void UpdateState()
        {
            if(_player.InputDirection.sqrMagnitude < 0.01f)
            {
                _player.StateContext.TransitionTo(_player.StateContext.IdleState);
            }

            _player.MoveSystem.Move();
        }

        public void ExitState()
        {
            _player.Anim.SetFloat("Speed", 0f);
        }
    }   
}


// 세이브
// public override void HandleInput(Vector3 direction)
// {
//     if (direction.sqrMagnitude < 0.01f)
//     {
//         player.SetState(PlayerStateType.Idle);
//     }

//     MoveDirectly(direction);
// }

// public override void OnDash()
// {
//     if(player.Stat.CurrentDashCount > 0)
//         player.SetState(PlayerStateType.Dash);
// }

// private void MoveDirectly(Vector3 direction)
// {
//     var slopeInfo = GetSlopeInfo(direction);

//     // 경사면 가파른 경우 못올라감
//     if (slopeInfo.angle > 45f && IsGrounded())
//     {
//         Debug.Log("경사면 너무 가파름");
//         return;
//     }

//     LookAt(direction);

//     // 경사면 속도 보정
//     if (slopeInfo.onSlope && IsGrounded())
//     {
//         Vector3 slopeDir = Vector3.ProjectOnPlane(direction, slopeInfo.normal);
//         player.rigid.velocity = slopeDir * player.Stat.CurrentMoveSpeed;
//     }
//     else
//     {
//         //평소 땅 위
//         Vector3 moveVelocity = new Vector3(direction.x, 0f, direction.z) * player.Stat.CurrentMoveSpeed;
//         player.rigid.velocity = new Vector3(moveVelocity.x, player.rigid.velocity.y, moveVelocity.z);
//     }

//     player.anim.SetFloat("Speed", direction.magnitude);

//     // 조건 추가: 일정 이상 이동할 때만 재생, 재생한 적 없을 때만
//     // if (direction.magnitude > 0.1f && !hasPlayedWalkSound)
//     // {
//     //     SoundManager.Instance.PlaySound(0);
//     //     hasPlayedWalkSound = true;
//     // }
//     // else if (direction.magnitude <= 0.1f)
//     // {
//     //     hasPlayedWalkSound = false;
//     // }
// }

// // 시야 처리
// private void LookAt(Vector3 direction)
// {
//     if (direction != Vector3.zero)
//     {
//         Debug.Log($"NearestEnemy: {player.scanner.NearestEnemy}, isAttacking: {player.AttackSystem.IsAttacking}");
//         if (player.scanner.NearestEnemy != null && player.AttackSystem.IsAttacking)
//         {
//             Debug.Log("플레이어가 적을 스캔함");

//             //적의 위치로 회전
//             direction = player.scanner.NearestEnemy.transform.position - player.transform.position;
//             direction.y = 0f;
//         }

//         Quaternion targetAngle = Quaternion.LookRotation(direction);
//         player.rigid.rotation = targetAngle;
//     }
// }

// // 땅 위에 있는지 체크
// private bool IsGrounded()
// {
//     return Physics.Raycast(player.transform.position, Vector3.down, 1.1f);
// }

// // 경사면 정보 가져오기
// private SlopeInfo GetSlopeInfo(Vector3 direction)
// {
//     RaycastHit hit;
//     SlopeInfo info = new SlopeInfo
//     {
//         onSlope = false,
//         angle = 0f,
//         normal = Vector3.up
//     };

//     Vector3 rayDir = Vector3.down + direction.normalized * 0.5f;

//     if (Physics.Raycast(player.transform.position, rayDir.normalized, out hit, 1.5f))
//     {
//         float angle = Vector3.Angle(hit.normal, Vector3.up);
//         info.onSlope = angle > 0 && angle < 45f;
//         info.angle = angle;
//         info.normal = hit.normal;
//     }

//     return info;
// }