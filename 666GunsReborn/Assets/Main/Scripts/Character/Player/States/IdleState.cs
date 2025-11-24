<<<<<<< HEAD
using UnityEngine;

namespace Character.Player
{
    public class IdleState : IState
    {
        private Player _player;

        public IdleState(Player player)
        {
            _player = player;
        }

        public void EnterState()
        {
            
        }

        public void UpdateState()
        {
            if(_player.InputDirection.sqrMagnitude > 0.01f)
            {
                _player.StateContext.TransitionTo(_player.StateContext.MoveState);
            }
        }

        public void ExitState()
        {
            
        }
    }
}

// 세이브
// // 정지 상태
// public class IdleState : PlayerStateBase
// {
//     public IdleState(Player player) : base(player)
//     {
//         Debug.Log("IdleState 생성");
//         Debug.Log("Player 초기화 : " + this.player.name);

//         var scenePlayer = GameObject.FindObjectOfType<Player>();
//         Debug.Log($"State.player ID = {player?.GetInstanceID()}");
//         Debug.Log($"Scene Player ID = {scenePlayer?.GetInstanceID()}");
//     }

//     public override void HandleInput(Vector3 direction)
//     {
//         if (direction.sqrMagnitude > 0.01f)
//         {
//             player.SetState(PlayerStateType.Move);
//         }

//         // 경사면 체크(경사면이면 미끌어지는 현상 방지)
//         var slopeInfo = GetSlopeInfo(direction);
//         if (slopeInfo.onSlope && IsGrounded())
//         {
//             player.rigid.velocity = Vector3.zero;
//         }
//     }

//     public override void OnDash()
//     {
//         if(player.Stat.CurrentDashCount > 0)
//             player.SetState(PlayerStateType.Dash);
//     }


//     // 플레이어 바닥이 경사인지 체크
//     private SlopeInfo GetSlopeInfo(Vector3 direction)
//     {
//         RaycastHit hit;
//         SlopeInfo info = new SlopeInfo
//         {
//             onSlope = false,
//             angle = 0f,
//             normal = Vector3.up
//         };

//         Vector3 rayDir = Vector3.down + direction.normalized * 0.5f;
//         if (player == null) {
//             Debug.Log("Unity null (Destroy됨)");
//         }
//         if (Object.ReferenceEquals(player, null))
//         {
//             Debug.Log("C# 레벨에서도 완전 null");
//         }

//         if (Physics.Raycast(player.transform.position, rayDir.normalized, out hit, 1.5f))
//         {
//             float angle = Vector3.Angle(hit.normal, Vector3.up);
//             info.onSlope = angle > 0 && angle < 45f;
//             info.angle = angle;
//             info.normal = hit.normal;
//         }

//         return info;
//     }
    
//     private bool IsGrounded()
//     {
//         return Physics.Raycast(player.transform.position, Vector3.down, 1.1f);
//     }
=======
using UnityEngine;

namespace Character.Player
{
    public class IdleState : IState
    {
        private Player _player;

        public IdleState(Player player)
        {
            _player = player;
        }

        public void EnterState()
        {
            
        }

        public void UpdateState()
        {
            if(_player.InputDirection.sqrMagnitude > 0.01f)
            {
                _player.StateContext.TransitionTo(_player.StateContext.MoveState);
            }
        }

        public void ExitState()
        {
            
        }
    }
}

// 세이브
// // 정지 상태
// public class IdleState : PlayerStateBase
// {
//     public IdleState(Player player) : base(player)
//     {
//         Debug.Log("IdleState 생성");
//         Debug.Log("Player 초기화 : " + this.player.name);

//         var scenePlayer = GameObject.FindObjectOfType<Player>();
//         Debug.Log($"State.player ID = {player?.GetInstanceID()}");
//         Debug.Log($"Scene Player ID = {scenePlayer?.GetInstanceID()}");
//     }

//     public override void HandleInput(Vector3 direction)
//     {
//         if (direction.sqrMagnitude > 0.01f)
//         {
//             player.SetState(PlayerStateType.Move);
//         }

//         // 경사면 체크(경사면이면 미끌어지는 현상 방지)
//         var slopeInfo = GetSlopeInfo(direction);
//         if (slopeInfo.onSlope && IsGrounded())
//         {
//             player.rigid.velocity = Vector3.zero;
//         }
//     }

//     public override void OnDash()
//     {
//         if(player.Stat.CurrentDashCount > 0)
//             player.SetState(PlayerStateType.Dash);
//     }


//     // 플레이어 바닥이 경사인지 체크
//     private SlopeInfo GetSlopeInfo(Vector3 direction)
//     {
//         RaycastHit hit;
//         SlopeInfo info = new SlopeInfo
//         {
//             onSlope = false,
//             angle = 0f,
//             normal = Vector3.up
//         };

//         Vector3 rayDir = Vector3.down + direction.normalized * 0.5f;
//         if (player == null) {
//             Debug.Log("Unity null (Destroy됨)");
//         }
//         if (Object.ReferenceEquals(player, null))
//         {
//             Debug.Log("C# 레벨에서도 완전 null");
//         }

//         if (Physics.Raycast(player.transform.position, rayDir.normalized, out hit, 1.5f))
//         {
//             float angle = Vector3.Angle(hit.normal, Vector3.up);
//             info.onSlope = angle > 0 && angle < 45f;
//             info.angle = angle;
//             info.normal = hit.normal;
//         }

//         return info;
//     }
    
//     private bool IsGrounded()
//     {
//         return Physics.Raycast(player.transform.position, Vector3.down, 1.1f);
//     }
>>>>>>> origin/main
// }   