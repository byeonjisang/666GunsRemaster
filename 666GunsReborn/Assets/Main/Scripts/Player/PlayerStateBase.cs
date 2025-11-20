using UnityEngine;

// 상태 패턴 베이스
public abstract class PlayerStateBase
{
    protected Player player;

    public PlayerStateBase(Player player)
    {
        this.player = player;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public abstract void HandleInput(Vector3 direction);
    public virtual void Update() { }
}

// 정지 상태
public class IdleState : PlayerStateBase
{
    public IdleState(Player player) : base(player)
    {
        Debug.Log("IdleState 생성");
        Debug.Log("Player 초기화 : " + this.player.name);

        var scenePlayer = GameObject.FindObjectOfType<Player>();
        Debug.Log($"State.player ID = {player?.GetInstanceID()}");
        Debug.Log($"Scene Player ID = {scenePlayer?.GetInstanceID()}");
    }

    public override void HandleInput(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            player.SetState(PlayerStateType.Move);
        }

        // 경사면 체크(경사면이면 미끌어지는 현상 방지)
        var slopeInfo = GetSlopeInfo(direction);
        if (slopeInfo.onSlope && IsGrounded())
        {
            player.rigid.velocity = Vector3.zero;
        }
    }

    // 플레이어 바닥이 경사인지 체크
    private SlopeInfo GetSlopeInfo(Vector3 direction)
    {
        RaycastHit hit;
        SlopeInfo info = new SlopeInfo
        {
            onSlope = false,
            angle = 0f,
            normal = Vector3.up
        };

        Vector3 rayDir = Vector3.down + direction.normalized * 0.5f;
        if (player == null) {
            Debug.Log("Unity null (Destroy됨)");
        }
        if (Object.ReferenceEquals(player, null))
        {
            Debug.Log("C# 레벨에서도 완전 null");
        }

        if (Physics.Raycast(player.transform.position, rayDir.normalized, out hit, 1.5f))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            info.onSlope = angle > 0 && angle < 45f;
            info.angle = angle;
            info.normal = hit.normal;
        }

        return info;
    }
    
    private bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, 1.1f);
    }

}
// 이동 상태
public class MoveState : PlayerStateBase
{
    public MoveState(Player player) : base(player) { }

    public override void HandleInput(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.01f)
        {
            player.SetState(PlayerStateType.Idle);
        }

        MoveDirectly(direction);
    }

    private void MoveDirectly(Vector3 direction)
    {
        var slopeInfo = GetSlopeInfo(direction);

        // 경사면 가파른 경우 못올라감
        if (slopeInfo.angle > 45f && IsGrounded())
        {
            Debug.Log("경사면 너무 가파름");
            return;
        }

        LookAt(direction);

        // 경사면 속도 보정
        if (slopeInfo.onSlope && IsGrounded())
        {
            Vector3 slopeDir = Vector3.ProjectOnPlane(direction, slopeInfo.normal);
            player.rigid.velocity = slopeDir * player.stat.CurrentMoveSpeed;
        }
        else
        {
            //평소 땅 위
            Vector3 moveVelocity = new Vector3(direction.x, 0f, direction.z) * player.stat.CurrentMoveSpeed;
            player.rigid.velocity = new Vector3(moveVelocity.x, player.rigid.velocity.y, moveVelocity.z);
        }

        player.anim.SetFloat("Speed", direction.magnitude);

        // 조건 추가: 일정 이상 이동할 때만 재생, 재생한 적 없을 때만
        // if (direction.magnitude > 0.1f && !hasPlayedWalkSound)
        // {
        //     SoundManager.Instance.PlaySound(0);
        //     hasPlayedWalkSound = true;
        // }
        // else if (direction.magnitude <= 0.1f)
        // {
        //     hasPlayedWalkSound = false;
        // }
    }

    // 시야 처리
    private void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Debug.Log($"NearestEnemy: {player.scanner.NearestEnemy}, isAttacking: {player.AttackSystem.IsAttacking}");
            if (player.scanner.NearestEnemy != null && player.AttackSystem.IsAttacking)
            {
                Debug.Log("플레이어가 적을 스캔함");

                //적의 위치로 회전
                direction = player.scanner.NearestEnemy.transform.position - player.transform.position;
                direction.y = 0f;
            }

            Quaternion targetAngle = Quaternion.LookRotation(direction);
            player.rigid.rotation = targetAngle;
        }
    }

    // 땅 위에 있는지 체크
    private bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, 1.1f);
    }

    // 경사면 정보 가져오기
    private SlopeInfo GetSlopeInfo(Vector3 direction)
    {
        RaycastHit hit;
        SlopeInfo info = new SlopeInfo
        {
            onSlope = false,
            angle = 0f,
            normal = Vector3.up
        };

        Vector3 rayDir = Vector3.down + direction.normalized * 0.5f;

        if (Physics.Raycast(player.transform.position, rayDir.normalized, out hit, 1.5f))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            info.onSlope = angle > 0 && angle < 45f;
            info.angle = angle;
            info.normal = hit.normal;
        }

        return info;
    }
}
// 대쉬 상태
public class DashState : PlayerStateBase
{
    public DashState(Player player) : base(player) { }

    public override void HandleInput(Vector3 direction)
    {
        // 대쉬 개수 감소
        player.stat.CurrentDashCount--;

        // 이동을 하고 있지 않다면 바라보는 방향으로 대쉬
        if (direction.sqrMagnitude < 0.1f)
            direction = player.transform.forward.normalized;
        // 대쉬 이동
        player.rigid.velocity = direction * player.stat.CurrentDashDistance;
    }
    public override void EnterState()
    {
        player.anim.SetBool("IsDash", true);
    }

    public override void ExitState()
    {
        player.anim.SetBool("IsDash", false);
    }
}