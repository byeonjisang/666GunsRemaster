﻿using System.Collections;
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
    public IdleState(Player player) : base(player) { }

    public override void HandleInput(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            player.SetState(PlayerStateType.Move);
        }

        player.Idle(direction);
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

        player.MoveDirectly(direction);
    }
}
// 대쉬 상태
public class DashState : PlayerStateBase
{
    public DashState(Player players) : base(players) { }

    public override void HandleInput(Vector3 direction) { }
}